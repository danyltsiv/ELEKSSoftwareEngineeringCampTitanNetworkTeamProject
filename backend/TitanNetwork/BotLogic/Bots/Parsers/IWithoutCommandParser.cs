using System.Collections.Generic;

namespace TitanWcfService.Services.Bots.Parsers
{
    /// <summary>
    /// Interface IWithoutCommandParser
    /// </summary>
    public interface IWithoutCommandParser
    {
        /// <summary>
        /// Mathes the expressions.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        List<string> MathExpressions(string message);
    }
}
