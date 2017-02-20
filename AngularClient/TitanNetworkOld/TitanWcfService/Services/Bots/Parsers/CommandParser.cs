using System;
using System.Collections.Generic;
using System.Linq;

namespace TitanWcfService.Services.Bots.Parsers
{
    public class CommandParser : ICommandParser
    {
        public List<string> Commands { get; set; }
        public List<string> ExceptionCommands { get; set; }
        public List<string> BokenMessage { get; set; }

        public CommandParser()
        {
            BokenMessage = new List<string>();
            ExceptionCommands = new List<string>() { "math" ,"url"};
            Commands = new List<string>() { "email", "help", "what is" };
            Commands.AddRange(ExceptionCommands);
        }

        public Dictionary<string, List<string>> ParsMessage(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var result = new Dictionary<string, List<string>>();
            const char begin = '[';
            const char start = '$';
            const char end = ']';
            var output = message.Split(start, end);
            Commands?.ForEach(command => result.Add(command, new List<string>()));
            BokenMessage.Clear();
            BokenMessage = output.ToList();
            foreach (var key in result.Keys)
            {
                BokenMessage.ForEach(delegate (string element)
                {
                    if (!element.Contains(key)) return;
                    var data = element.Split(begin);
                    var temp = (data.Length > 1) ? data[1] : key;
                    result[key].Add(temp);
                });
            }
            return result;
        }
    }
}
