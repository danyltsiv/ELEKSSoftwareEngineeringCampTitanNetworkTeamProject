using HtmlAgilityPack;

namespace TitanWcfService.Contracts.Parsers
{
    public interface IHTMLAnswerParser
    {
        string ParceAnswerToText(HtmlDocument htmlDocument);
        string ParceAnswerToHTML(HtmlDocument htmlDocument);
    }
}