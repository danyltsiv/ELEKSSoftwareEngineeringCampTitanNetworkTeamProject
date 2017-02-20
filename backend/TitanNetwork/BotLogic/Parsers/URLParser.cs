using System;
using System.Text.RegularExpressions;


namespace TitanWcfService.Services.Parsers
{
    /// <summary>
    /// Class URLParser.
    /// </summary>
    public partial class URLParser
    {
        /// <summary>
        /// The _url reg
        /// </summary>
        private readonly Regex _urlReg = new Regex(String.Concat("[\\w.]+\\.(com|com.ua|ru|if.ua",
            "|net|org|biz|edu|gov|info|int|net|pro)((/\\w+)*)?|(((http://)?[wW][wW][wW]\\.)",
            "|(http://))\\w+(\\.\\w+)+((/\\w+)*)?"));
        /// <summary>
        /// The _connector
        /// </summary>
        private readonly TitanWcfService.Services.InternetServices.Connector _connector = new TitanWcfService.Services.InternetServices.Connector();

        /// <summary>
        /// Returns the text in which the link is anchored with the corresponding caption
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>System.String.</returns>
        public string Parse(string text)
        {
            var matches = _urlReg.Matches(text);

            if (matches.Count == 0)
            {
                return text;
            }

            foreach (var matched in matches)
            {
                var matchedUrl = matched.ToString();

                if (matchedUrl.Contains("http://"))
                {
                    _connector.SetURL(matchedUrl.ToString());
                }
                else
                {
                    _connector.SetURL($"http://{matchedUrl}");
                    matchedUrl = string.Concat("http://", matchedUrl);
                }

                var document = _connector.GetHtmlDocument();
                var caption = document.DocumentNode.SelectNodes("//title")[0].InnerHtml;

                var replacement = $"<a href='{matchedUrl}'>{caption}</a>";
                text = text.Replace(matched.ToString(), replacement);
            }
            return text;
        }
    }
}
