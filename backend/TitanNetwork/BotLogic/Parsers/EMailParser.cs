using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TitanWcfService.Services.Parsers
{
    /// <summary>
    /// Class EMailParser.
    /// </summary>
    public class EMailParser
    {
        /// <summary>
        /// The _url reg
        /// </summary>
        private readonly Regex _urlReg = new Regex(String.Concat(
        @"(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))"));

        /// <summary>
        /// Parses the specified text.
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
                var matchedMail = matched.ToString();
                var replacement = $"<a href='mailto:{matchedMail}'>{matchedMail}</a>";
                text = text.Replace(matchedMail, replacement);
            }
            return text;
        }
    }
}