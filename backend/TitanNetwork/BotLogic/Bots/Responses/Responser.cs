using System;
using System.Collections.Generic;
using System.Linq;

namespace TitanWcfService.Services.Bots.Responses
{
    /// <summary>
    /// Class Responser.
    /// </summary>
    public class Responser
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
        /// The response
        /// </summary>
        public string Response;

        /// <summary>
        /// Initializes a new instance of the <see cref="Responser"/> class.
        /// </summary>
        /// <param name="commands">The commands.</param>
        /// <param name="exceptionCommands">The exception commands.</param>
        public Responser(List<string> commands, List<string> exceptionCommands)
        {
            ExceptionCommands = exceptionCommands;
            Commands = commands;
            Response = string.Empty;
        }

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="bokenMessage">The boken message.</param>
        public void CreateMessage(List<string> bokenMessage)
        {
            bokenMessage.ForEach(MakeMessageWithoutCommands);
        }

        /// <summary>
        /// Makes the message without commands.
        /// </summary>
        /// <param name="element">The element.</param>
        private void MakeMessageWithoutCommands(string element)
        {
            ExceptionCommands.ForEach(delegate (string e)
            {
                if (element.IndexOf(e, StringComparison.Ordinal) != 0) return;
                Response += string.Concat("$$", e);
            });
            if (IsContaintCommand(element)) return;
            if (string.IsNullOrWhiteSpace(element)) return;
            Response += element;
        }

        /// <summary>
        /// Determines whether [is containt command] [the specified command].
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns><c>true</c> if [is containt command] [the specified command]; otherwise, <c>false</c>.</returns>
        private bool IsContaintCommand(string command)
        {
            return Commands.Any(element => command.IndexOf(element, StringComparison.Ordinal) == 0);
        }

        /// <summary>
        /// Sets the result instead command.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public void SetResultInsteadCommand(string from, string to)
        {
            from = string.Concat("$$", from);
            SetResultInsteadWithoutCommand(from, to);
        }

        /// <summary>
        /// Sets the result instead without command.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public void SetResultInsteadWithoutCommand(string from, string to)
        {
            Response = Response.Replace(from, to);
        }
    }
}
