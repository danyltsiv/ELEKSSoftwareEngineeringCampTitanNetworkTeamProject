using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TitanWcfService.Services.Bots.Parsers
{
    /// <summary>
    /// Class WithoutCommandParser.
    /// </summary>
    public class WithoutCommandParser
    {
        /// <summary>
        /// The _math regex
        /// </summary>
        private static readonly Regex _mathRegex = new Regex(string.Concat("((\\(\\s*)?\\-?\\d+(\\s*,\\d*)?)",
            "(\\s*[+\\-\\*/\\^]\\s*(\\(\\s*)?\\-?\\d+(\\s*,\\d*)?(\\s*\\))?)+"));

        /// <summary>
        /// The _url regex
        /// </summary>
        private static readonly Regex _urlRegex = new Regex(string.Concat("[\\w.]+\\.(com|com.ua|ru|if.ua",
           "|net|org|biz|edu|gov|info|int|net|pro)((/\\w+)*)?|(((http://)?[wW][wW][wW]\\.)",
           "|(http://))\\w+(\\.\\w+)+((/\\w+)*)?"));


        /// <summary>
        /// Mathes the expressions.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> MathExpressions(string message)
        {
            var matches = _mathRegex.Matches(message)
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();
            return matches;
        }

        /// <summary>
        /// URLs the expressions.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> UrlExpressions(string message)
        {
            var matches = _urlRegex.Matches(message).Cast<Match>()
                .Select(m => m.Value)
                .ToList();
            return matches;
        }
    }
}
