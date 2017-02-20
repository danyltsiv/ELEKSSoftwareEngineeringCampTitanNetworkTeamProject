using System.Collections.Generic;

namespace TitanWcfService.Services.Bots.Managers
{
    /// <summary>
    /// Delegate Del
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>List&lt;System.String&gt;.</returns>
    public delegate List<string> Del(string message);

    /// <summary>
    /// Class WithoutCommandManager.
    /// </summary>
    public class WithoutCommandManager
    {
        // private readonly Dictionary<string, List<string>> _manager;
        /// <summary>
        /// Gets or sets the exception commands.
        /// </summary>
        /// <value>The exception commands.</value>
        public List<string> ExceptionCommands { get; set; }

        private event Del delegator;

        /// <summary>
        /// Initializes a new instance of the <see cref="WithoutCommandManager"/> class.
        /// </summary>
        /// <param name="exceptionCommands">The exception commands.</param>
        public WithoutCommandManager(List<string> exceptionCommands)
        {
            ExceptionCommands = exceptionCommands;
            delegator = Parsers.WithoutCommandParser.MathExpressions;
            delegator += Parsers.WithoutCommandParser.UrlExpressions;
        }

        /// <summary>
        /// Finds the expressions.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Dictionary&lt;System.String, List&lt;System.String&gt;&gt;.</returns>
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
