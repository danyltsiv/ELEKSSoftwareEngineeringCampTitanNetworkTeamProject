using System.Collections.Generic;

namespace TitanWcfService.Services.Bots.Parsers
{
    /// <summary>
    /// Interface ICommandParser
    /// </summary>
    public interface ICommandParser
    {
        /// <summary>
        /// Parses the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Dictionary&lt;System.String, List&lt;System.String&gt;&gt;.</returns>
        Dictionary<string, List<string>> ParseMessage(string message);
    }
}
