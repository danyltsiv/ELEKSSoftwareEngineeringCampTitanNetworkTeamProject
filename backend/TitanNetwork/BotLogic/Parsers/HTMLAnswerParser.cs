using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TitanWcfService.Services.Parsers
{
    public class HTMLAnswerParser
    {
        private enum Formats
        {
            Text,
            HTML
        }

        private IEnumerable<HtmlNode> GetNodes(string tagRegex, int numberOfNodes, HtmlDocument document)
        {
            IEnumerable<HtmlNode> nodes;
            try
            {
                nodes = document.DocumentNode.SelectNodes(tagRegex).Take(numberOfNodes);
            }
            catch (ArgumentNullException)
            {
                List<HtmlNode> list = new List<HtmlNode>();
                HtmlNode node = HtmlNode.CreateNode("<p>can't find information</p>");
                list.Add(node);
                nodes = list;
            }
            return nodes;
        }

        private IEnumerable<HtmlNode> ParceAnswer(HtmlDocument htmlDocument)
        {
            IEnumerable<HtmlNode> pNodes;
            pNodes = GetNodes("//p", 2, htmlDocument);
            if (pNodes.Count() < 2)
            {
                pNodes = GetNodes("//p | //ul", 2, htmlDocument);
            }
            return pNodes;
        }

        private string SetNodesInString(IEnumerable<HtmlNode> nodes, Formats format)
        {
            var answerBuilder = new StringBuilder();
            foreach (var nod in nodes)
            {
                switch (format)
                {
                    case Formats.HTML:
                        answerBuilder.Append(nod.InnerHtml);
                        break;
                    case Formats.Text:
                        answerBuilder.Append(nod.InnerText);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(format), format, null);
                }
            }
            return answerBuilder.ToString();
        }

        public string ParceAnswerToHTML(HtmlDocument htmlDocument)
        {
            return SetNodesInString(ParceAnswer(htmlDocument), Formats.HTML);
        }

        public string ParceAnswerToText(HtmlDocument htmlDocument)
        {
            return SetNodesInString(ParceAnswer(htmlDocument), Formats.Text);
        }
    }
}