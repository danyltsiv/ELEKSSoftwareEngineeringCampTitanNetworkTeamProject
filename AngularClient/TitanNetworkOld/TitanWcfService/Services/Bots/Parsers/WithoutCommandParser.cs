using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TitanWcfService.Services.Bots.Parsers
{
    public class WithoutCommandParser
    {
        private static readonly Regex _mathRegex = new Regex(string.Concat("((\\(\\s*)?\\-?\\d+(\\s*,\\d*)?)",
            "(\\s*[+\\-\\*/\\^]\\s*(\\(\\s*)?\\-?\\d+(\\s*,\\d*)?(\\s*\\))?)+"));

        private static readonly Regex _urlRegex = new Regex(string.Concat("[\\w.]+\\.(com|com.ua|ru|if.ua",
           "|net|org|biz|edu|gov|info|int|net|pro)((/\\w+)*)?|(((http://)?[wW][wW][wW]\\.)",
           "|(http://))\\w+(\\.\\w+)+((/\\w+)*)?"));


        public static List<string> MathExpressions(string message)
        {
            var matches = _mathRegex.Matches(message)
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();
            return matches;
        }

        public static List<string> UrlExpressions(string message)
        {
            var matches = _urlRegex.Matches(message).Cast<Match>()
                .Select(m => m.Value)
                .ToList();
            return matches;
        }
    }
}
