using System.Collections.Generic;

namespace TitanWcfService.Services.Bots.Parsers
{
    public interface ICommandParser
    {
        Dictionary<string, List<string>> ParsMessage(string message);
    }
}
