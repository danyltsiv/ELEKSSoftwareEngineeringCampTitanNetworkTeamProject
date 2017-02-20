using System.Collections.Generic;

namespace TitanWcfService.Services.Bots.Parsers
{
    public interface IWithoutCommandParser
    {
        List<string> MathExpressions(string message);
    }
}
