using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;



namespace TitanWcfService.Services.InternetServices
{
    /// <summary>
    /// Class Searcher.
    /// </summary>
    /// <seealso cref="TitanWcfService.Services.InternetServices.Connector" />
    public class Searcher : Connector
    {
        /// <summary>
        /// The _searcher URL
        /// </summary>
        private string _searcherURL = "http://google.com/search?q=";
        /// <summary>
        /// The _key words
        /// </summary>
        private string _keyWords;

        /// <summary>
        /// Sets the URL.
        /// </summary>
        public void SetURL()
        {
            URL = _searcherURL + _keyWords;
        }

        /// <summary>
        /// Sets the searcher engine.
        /// </summary>
        /// <param name="searcherURL">The searcher URL.</param>
        public void SetSearcherEngine(string searcherURL)
        {
            _searcherURL = searcherURL;
        }

        /// <summary>
        /// Sets the key words.
        /// </summary>
        /// <param name="keyWords">The key words.</param>
        public void SetKeyWords(string keyWords)
        {
            _keyWords = keyWords;
            SetURL();
        }

        /// <summary>
        /// Checks for empty links list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        private List<string> CheckForEmptyLinksList(List<string> list)
        {
            var isEmpty = !list.Any();
            if (isEmpty)
            {
                list.Add("sasi");
            }
            return list;
        }

        /// <summary>
        /// Gets the links.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetLinks()
        {
            var linksList = new List<string>();
            GetHtmlDocument();
            var htmlNode = HtmlDocument.DocumentNode;
            foreach (var link in htmlNode.SelectNodes("//a[@href]"))
            {
                var hrefValue = link.GetAttributeValue("href", string.Empty);
                if (hrefValue.ToUpper().Contains("GOOGLE") || !hrefValue.Contains("/url?q=") ||
                    !hrefValue.ToUpper().Contains("HTTPS://")) continue;
                var index = hrefValue.IndexOf("&", StringComparison.Ordinal);
                if (index <= 0) continue;
                hrefValue = hrefValue.Substring(0, index);
                linksList.Add(hrefValue.Replace("/url?q=", ""));
            }
            linksList = CheckForEmptyLinksList(linksList);
            linksList.Reverse();
            return linksList;
        }
    }
}