using System.Linq;


namespace TitanWcfService.Services.Bots.Commands.WhatIs
{
    public class Answerer : ICommander
    {
        private string _url;
        private readonly string[] _domeins = { "uk", "ru", "it", "de", "es" };
        private readonly InternetServices.Connector _connectorForAnswer;
        private readonly Services.Parsers.HTMLAnswerParser _parser;
        private string NotFoundMessage = "Sorry friend, I don't understnd what are you talking about";
        public Answerer()
        {
            _connectorForAnswer = new InternetServices.Connector();
            _parser = new Services.Parsers.HTMLAnswerParser();
        }

        private void CreateConnector(string question)
        {
            FindFromWikiConnector("en", question);
            var i = 0;
            while (CheckForNotFoundMessage(_connectorForAnswer) && i < _domeins.Length)
            {
                FindFromWikiConnector(_domeins[i], question);
                i++;
            }

            if (CheckForNotFoundMessage(_connectorForAnswer))
            {
                FindFromGoogle(question);
            }
        }

        private void FindFromWikiConnector(string domen, string question)
        {
            string wikiURL = $"https://{domen}.wikipedia.org/wiki/{question}";
            _url = wikiURL;
            _connectorForAnswer.SetURL(wikiURL);
        }

        private void FindFromGoogle(string question)
        {
            var searcher = new InternetServices.Searcher();
            searcher.SetKeyWords(question);
            _url = searcher.GetLinks().Last();
            _connectorForAnswer.SetURL(_url);
        }

        private bool CheckForNotFoundMessage(InternetServices.Connector connector)
        {
            connector.GetHtmlDocument();
            return connector.GetURL() == Constants.AppSettings.Instance.NotFoundURL;
        }

        public string AnswerText(string question)
        {
            CreateConnector(question);
            if (CheckForNotFoundMessage(_connectorForAnswer))
            {
                return NotFoundMessage;
            }
            return _parser.ParceAnswerToText(_connectorForAnswer.GetHtmlDocument());
        }

        public string AnswerHTML(string question)
        {
            CreateConnector(question);
            if (CheckForNotFoundMessage(_connectorForAnswer))
            {
                return NotFoundMessage;
            }
            var answer = $"{_parser.ParceAnswerToHTML(_connectorForAnswer.GetHtmlDocument())} \n {_url}";
            return answer;
        }

        public string Execute(string expression)
        {
            return AnswerText(expression);
        }
    }
}
