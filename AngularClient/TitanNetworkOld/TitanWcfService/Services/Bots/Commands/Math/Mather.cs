using System.Diagnostics.CodeAnalysis;

namespace TitanWcfService.Services.Bots.Commands.Math
{
    public class Mather : Services.Parsers.MathParser, ICommander
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public string Execute(string expression)
        {
            var RPNList = ConvertToRPN(expression);
            return CalculateTheRPNExpression(RPNList);
        }
    }
}
