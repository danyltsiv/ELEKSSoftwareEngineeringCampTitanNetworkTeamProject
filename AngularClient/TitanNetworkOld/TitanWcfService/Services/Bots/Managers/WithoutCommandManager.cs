using System.Collections.Generic;

namespace TitanWcfService.Services.Bots.Managers
{
    public delegate List<string> Del(string message);

    public class WithoutCommandManager
    {
        private readonly Dictionary<string, List<string>> _manager;
        public List<string> ExceptionCommands { get; set; }

        private event Del delegator;

        public WithoutCommandManager(List<string> exceptionCommands)
        {
            ExceptionCommands = exceptionCommands;
            delegator = Parsers.WithoutCommandParser.MathExpressions;
            delegator += Parsers.WithoutCommandParser.UrlExpressions;
        }

        public Dictionary<string, List<string>> FindExpressions(string message)
        {
            var result = new Dictionary<string, List<string>>();
            var index = 0;
            foreach (var @delegate in delegator.GetInvocationList())
            {
                var key = ExceptionCommands[index++];
                var del = (Del)@delegate;
                var data =  del(message);
                result.Add(key,data);
            }
            return result;
        }
    }

}
