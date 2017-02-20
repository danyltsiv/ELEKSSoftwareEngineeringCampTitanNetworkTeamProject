using System;
using System.Collections.Generic;
using System.Linq;
using TitanWcfService.Services.Bots.Managers;
using TitanWcfService.Services.Bots.Parsers;
using TitanWcfService.Services.Bots.Responses;

namespace BusinessLogic.ManualTests
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "2+2 $help [full] dsda $math [(2+3)*3] $what is [google]";
            Console.WriteLine(SendToBot(message));
            
            Console.ReadLine();
        }
        public static string SendToBot(string message)
        {
            var commandParser = new CommandParser();
            var manager = new CommandManager();
            var data = commandParser.ParseMessage(message);

            var response = new Responser(commandParser.Commands, commandParser.ExceptionCommands);
            response.CreateMessage(commandParser.SplittedMessage);

            var listWithAnswers = new List<string>();

            foreach (var temp in data)
            {
                foreach (var arg in temp.Value)
                {
                    var result = manager.Execute(temp.Key, arg);

                    if (commandParser.ExceptionCommands.Contains(temp.Key))
                    {
                        response.SetResultInsteadCommand(temp.Key, result);
                    }
                    else
                    {
                        listWithAnswers.Add(result);
                    }
                }
            }

            var wcm = new WithoutCommandManager(commandParser.ExceptionCommands);
            var data2 = wcm.FindExpressions(response.Response);
            foreach (var temp in data2)
            {
                foreach (var arg in temp.Value)
                {
                    var result = manager.Execute(temp.Key, arg);
                    response.SetResultInsteadWithoutCommand(arg, result);
                }
            }

            var returnResult = listWithAnswers.Aggregate((a, b) => a + "\n\n" + b) + "\n\n" + response.Response;
            return returnResult;
        }
    }
}