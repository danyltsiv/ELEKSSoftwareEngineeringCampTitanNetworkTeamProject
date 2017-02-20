using System.Diagnostics.CodeAnalysis;

namespace TitanWcfService.Services.Bots.Commands.Math
{
    /// <summary>
    /// Class Mather.
    /// </summary>
    /// <seealso cref="TitanWcfService.Services.Parsers.MathParser" />
    /// <seealso cref="TitanWcfService.Services.Bots.Commands.ICommander" />
    public class Mather : Services.Parsers.MathParser, ICommander
    {
        /// <summary>
        /// Executes the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>System.String.</returns>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public string Execute(string expression)
        {
            var RPNList = ConvertToRPN(expression);
            return CalculateTheRPNExpression(RPNList);
        }
    }
}
