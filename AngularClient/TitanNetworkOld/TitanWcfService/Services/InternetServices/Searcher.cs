using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;



namespace TitanWcfService.Services.InternetServices
{
    public class Searcher: Connector, Contracts.InternetServices.ISearcher
    {
        private string _searcherURL = "http://google.com/search?q=";
        private string _keyWords;

        public void SetURL()
        {
            URL = _searcherURL + _keyWords;
        }

        public void SetSearcherEngine(string searcherURL)
        {
            _searcherURL = searcherURL;
        }

        public void SetKeyWords(string keyWords)
        {
            _keyWords = keyWords;
            SetURL();
        }

        private List<string> CheckForEmptyLinksList(List<string> list)
        {
            var isEmpty = !list.Any();
            if (isEmpty)
            {
                list.Add(Constants.AppSettings.Instance.NotFoundURL);
            }
            return list;
        }

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