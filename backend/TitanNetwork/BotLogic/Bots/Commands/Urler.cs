using HtmlAgilityPack;
using System;

namespace TitanWcfService.Services.Bots.Commands.Url
{
    /// <summary>
    /// Class Urler.
    /// </summary>
    /// <seealso cref="TitanWcfService.Services.Bots.Commands.ICommander" />
    public class Urler : ICommander
    {
        /// <summary>
        /// The _connector
        /// </summary>
        private readonly InternetServices.Connector _connector = new TitanWcfService.Services.InternetServices.Connector();

        /// <summary>
        /// Returns the text in which the link is anchored with the corresponding caption
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>System.String.</returns>
        public string Execute(string url)
        {
            if (url.Contains("http://") || url.Contains("https://"))
            {
                _connector.SetURL(url.ToString());
            }
            else
            {
                _connector.SetURL(String.Format("https://{0}", url));
                url = String.Concat("https://", url);
            }

            HtmlDocument document = _connector.GetHtmlDocument();
            var caption = document.DocumentNode.SelectNodes("//title")[0].InnerHtml;

            return String.Format("<a href='{0}'>{1}</a>", url, caption);
        }
    }
}
