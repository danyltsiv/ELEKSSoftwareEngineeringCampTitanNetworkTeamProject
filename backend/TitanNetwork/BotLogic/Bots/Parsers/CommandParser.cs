using System;
using System.Collections.Generic;
using System.Linq;

namespace TitanWcfService.Services.Bots.Parsers
{
    /// <summary>
    /// Class CommandParser.
    /// </summary>
    /// <seealso cref="TitanWcfService.Services.Bots.Parsers.ICommandParser" />
    public class CommandParser : ICommandParser
    {
        /// <summary>
        /// Gets or sets the commands.
        /// </summary>
        /// <value>The commands.</value>
        public List<string> Commands { get; set; }
        /// <summary>
        /// Gets or sets the exception commands.
        /// </summary>
        /// <value>The exception commands.</value>
        public List<string> ExceptionCommands { get; set; }
        /// <summary>
        /// Gets or sets the splitted message.
        /// </summary>
        /// <value>The splitted message.</value>
        public List<string> SplittedMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParser"/> class.
        /// </summary>
        public CommandParser()
        {
            SplittedMessage = new List<string>();
            ExceptionCommands = new List<string>() { "math" ,"url"};
            Commands = new List<string>() { "email", "help", "what is" };
            Commands.AddRange(ExceptionCommands);
        }

        /// <summary>
        /// Parses the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Dictionary&lt;System.String, List&lt;System.String&gt;&gt;.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Dictionary<string, List<string>> ParseMessage(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var result = new Dictionary<string, List<string>>();
            const char begin = '[';
            const char start = '$';
            const char end = ']';
            var output = message.Split(start, end);
            Commands?.ForEach(command => result.Add(command, new List<string>()));
            SplittedMessage.Clear();
            SplittedMessage = output.ToList();
            foreach (var key in result.Keys)
            {
                SplittedMessage.ForEach(delegate (string element)
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
