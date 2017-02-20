using HtmlAgilityPack;
using System.IO;
using System.Net;
using System.Text;

namespace TitanWcfService.Services.InternetServices
{
    /// <summary>
    /// Class Connector.
    /// </summary>
    public class Connector
    {
        /// <summary>
        /// The URL
        /// </summary>
        protected string URL;
        /// <summary>
        /// The HTML document
        /// </summary>
        public HtmlDocument HtmlDocument;
        /// <summary>
        /// The _request
        /// </summary>
        HttpWebRequest _request;
        /// <summary>
        /// The _response
        /// </summary>
        HttpWebResponse _response;
        /// <summary>
        /// Sets the URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public void SetURL(string url)
        {
            URL = url;
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetURL()
        {
            return URL;
        }

        /// <summary>
        /// Creates the response.
        /// </summary>
        /// <returns>HttpWebResponse.</returns>
        public HttpWebResponse CreateResponse()
        {
            try
            {
                _request = (HttpWebRequest)WebRequest.Create(URL);
                _response = (HttpWebResponse)_request.GetResponse();
            }
            catch (WebException)
            {
                URL = "sasi";
                _request = (HttpWebRequest)WebRequest.Create(URL);
                _response = (HttpWebResponse)_request.GetResponse();
            }
            return _response;
        }

        /// <summary>
        /// Creates the only header response.
        /// </summary>
        /// <returns>HttpWebResponse.</returns>
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
                URL = "sasi";
                _request = (HttpWebRequest)WebRequest.Create(URL);
                _request.Method = "HEAD";
                _response = (HttpWebResponse)_request.GetResponse();
            }
            return _response;
        }

        /// <summary>
        /// Gets the string from responce.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>System.String.</returns>
        public string GetStringFromResponce(HttpWebResponse response)
        {
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var responseString = reader.ReadToEnd();
            return responseString;
        }

        /// <summary>
        /// Gets the string header from HTML.
        /// </summary>
        /// <returns>System.String.</returns>
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

        /// <summary>
        /// Gets the HTML document.
        /// </summary>
        /// <returns>HtmlDocument.</returns>
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