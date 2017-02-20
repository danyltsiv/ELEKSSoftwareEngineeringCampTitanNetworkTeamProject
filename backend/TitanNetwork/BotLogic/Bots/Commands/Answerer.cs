using System.Linq;


namespace TitanWcfService.Services.Bots.Commands.WhatIs
{
    /// <summary>
    /// Class Answerer.
    /// </summary>
    /// <seealso cref="TitanWcfService.Services.Bots.Commands.ICommander" />
    public class Answerer : ICommander
    {
        /// <summary>
        /// The _url
        /// </summary>
        private string _url;
        /// <summary>
        /// The _domeins
        /// </summary>
        private readonly string[] _domeins = { "uk", "ru", "it", "de", "es" };
        /// <summary>
        /// The _connector for answer
        /// </summary>
        private readonly InternetServices.Connector _connectorForAnswer;
        /// <summary>
        /// The _parser
        /// </summary>
        private readonly Services.Parsers.HTMLAnswerParser _parser;
        /// <summary>
        /// The not found message
        /// </summary>
        private string NotFoundMessage = "Sorry friend, I don't understnd what are you talking about";
        /// <summary>
        /// Initializes a new instance of the <see cref="Answerer"/> class.
        /// </summary>
        public Answerer()
        {
            _connectorForAnswer = new InternetServices.Connector();
            _parser = new Services.Parsers.HTMLAnswerParser();
        }

        /// <summary>
        /// Creates the connector.
        /// </summary>
        /// <param name="question">The question.</param>
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

        /// <summary>
        /// Finds from wiki connector.
        /// </summary>
        /// <param name="domen">The domen.</param>
        /// <param name="question">The question.</param>
        private void FindFromWikiConnector(string domen, string question)
        {
            string wikiURL = $"https://{domen}.wikipedia.org/wiki/{question}";
            _url = wikiURL;
            _connectorForAnswer.SetURL(wikiURL);
        }

        /// <summary>
        /// Finds from google.
        /// </summary>
        /// <param name="question">The question.</param>
        private void FindFromGoogle(string question)
        {
            var searcher = new InternetServices.Searcher();
            searcher.SetKeyWords(question);
            _url = searcher.GetLinks().Last();
            _connectorForAnswer.SetURL(_url);
        }

        /// <summary>
        /// Checks for not found message.
        /// </summary>
        /// <param name="connector">The connector.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool CheckForNotFoundMessage(InternetServices.Connector connector)
        {
            connector.GetHtmlDocument();
            return connector.GetURL() == "sasi";
        }

        /// <summary>
        /// Answers the text.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <returns>System.String.</returns>
        public string AnswerText(string question)
        {
            CreateConnector(question);
            if (CheckForNotFoundMessage(_connectorForAnswer))
            {
                return NotFoundMessage;
            }
            return _parser.ParceAnswerToText(_connectorForAnswer.GetHtmlDocument());
        }

        /// <summary>
        /// Answers the HTML.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <returns>System.String.</returns>
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

        /// <summary>
        /// Executes the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>System.String.</returns>
        public string Execute(string expression)
        {
            return AnswerText(expression);
        }
    }
}
