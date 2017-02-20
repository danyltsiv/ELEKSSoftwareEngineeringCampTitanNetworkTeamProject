using System;
using System.Collections.Generic;
using System.Linq;

namespace TitanWcfService.Services.Bots.Responses
{
    public class Responser
    {
        public List<string> Commands { get; set; }
        public List<string> ExceptionCommands { get; set; }
        public string Response;

        public Responser(List<string> commands, List<string> exceptionCommands)
        {
            ExceptionCommands = exceptionCommands;
            Commands = commands;
            Response = string.Empty;
        }

        public void CreateMessage(List<string> bokenMessage)
        {
            bokenMessage.ForEach(MakeMessageWithoutCommands);
        }

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

        private bool IsContaintCommand(string command)
        {
            return Commands.Any(element => command.IndexOf(element, StringComparison.Ordinal) == 0);
        }

        public void SetResultInsteadCommand(string from, string to)
        {
            from = string.Concat("$$", from);
            SetResultInsteadWithoutCommand(from, to);
        }

        public void SetResultInsteadWithoutCommand(string from, string to)
        {
            Response = Response.Replace(from, to);
        }
    }
}
