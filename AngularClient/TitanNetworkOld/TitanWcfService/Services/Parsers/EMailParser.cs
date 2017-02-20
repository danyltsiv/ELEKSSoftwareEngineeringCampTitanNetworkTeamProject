using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TitanWcfService.Services.Parsers
{
    public class EMailParser
    {
        private readonly Regex _urlReg = new Regex(String.Concat(
        @"(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))"));

        public int Parse(string text)
        {
            var matches = _urlReg.Matches(text);
            return matches.Count;
        }
    }
}