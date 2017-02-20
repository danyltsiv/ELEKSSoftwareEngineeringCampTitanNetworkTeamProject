using HtmlAgilityPack;
using System.IO;
using System.Net;
using System.Text;



namespace TitanWcfService.Services.InternetServices
{
    public class Connector: Contracts.InternetServices.IConnector
    {
        protected string URL;
        public HtmlDocument HtmlDocument;
        HttpWebRequest _request;
        HttpWebResponse _response;
        public void SetURL(string url)
        {
            URL = url;
        }

        public string GetURL()
        {
            return URL;
        }
        
        public HttpWebResponse CreateResponse()
        {
            try
            {
                _request = (HttpWebRequest)WebRequest.Create(URL);
                _response = (HttpWebResponse)_request.GetResponse();
            }
            catch (WebException)
            {
                URL = Constants.AppSettings.Instance.NotFoundURL;
                _request = (HttpWebRequest)WebRequest.Create(URL);
                _response = (HttpWebResponse)_request.GetResponse();
            }
            return _response;
        }

        public HttpWebResponse CreateOnlyHeaderResponse()
        {
            try
            {
                _request = (HttpWebRequest)WebRequest.Create(URL);
                _request.Method = "HEAD";
                _response = (HttpWebResponse)_request.GetResponse();
            }
            catch (WebException)
            {
                URL = TitanWcfService.Constants.AppSettings.Instance.NotFoundURL;
                _request = (HttpWebRequest)WebRequest.Create(URL);
                _request.Method = "HEAD";
                _response = (HttpWebResponse)_request.GetResponse();
            }
            return _response;
        }

        public string GetStringFromResponce(HttpWebResponse response)
        {
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var responseString = reader.ReadToEnd();
            return responseString;
        }

        public string GetStringHeaderFromHTML()
        {
            CreateOnlyHeaderResponse();
            var builder = new StringBuilder();
            foreach (string header in _response.Headers)
            {
                builder.Append($"{header}:{_response.Headers[header]}");
            }
            return builder.ToString();
        }

        public HtmlDocument GetHtmlDocument()
        {
            var html = new HtmlDocument();
            CreateResponse();
            var htmlText = GetStringFromResponce(_response);
            html.LoadHtml(htmlText);
            HtmlDocument = html;
            return html;
        }
    }
}