using HtmlAgilityPack;
using System.Net;

namespace TitanWcfService.Contracts.InternetServices
{
    public interface IConnector
    {
        void SetURL(string url);
        HttpWebResponse CreateResponse();
        string GetStringFromResponce(HttpWebResponse response);
        HtmlDocument GetHtmlDocument();
    }
}