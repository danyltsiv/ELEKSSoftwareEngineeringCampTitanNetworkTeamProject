namespace TitanWcfService.Services.Bots.Commands
{
    /// <summary>
    /// Interface ICommander
    /// </summary>
    public interface ICommander
    {
        /// <summary>
        /// Executes the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>System.String.</returns>
        string Execute(string expression);
    }
}
