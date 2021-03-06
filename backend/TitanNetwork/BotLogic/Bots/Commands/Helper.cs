﻿namespace TitanWcfService.Services.Bots.Commands.Help
{
    /// <summary>
    /// Class Helper.
    /// </summary>
    /// <seealso cref="TitanWcfService.Services.Bots.Commands.ICommander" />
    public class Helper : ICommander
    {
        /// <summary>
        /// Abouts the command.
        /// </summary>
        /// <param name="fullOrShortInformation">The full or short information.</param>
        /// <returns>System.String.</returns>
        protected virtual string AboutCommand(string fullOrShortInformation)
        {
            string result;

            if (string.IsNullOrEmpty(fullOrShortInformation))
            {
                result = "$help or $help [full] \n"
                                  + "$math [ expression ]\n"
                                  + "$email [ to / subject / message]\n"
                                  + "$email [ from/ from password / to / subject / message ]";
            }
            else
            {
                result = "$help or $help [full]  -> about command\n"
                                  + "$math [ expression ] -> calculation expression \n"
                                  + "$email [ to / subject / message] -> bot send e-mail message  \n"
                                  + "$email [ from/ from password / to / subject / message ] -> send e-mail message from own e-email";
            }
            return result;
        }

        /// <summary>
        /// Executes the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>System.String.</returns>
        public string Execute(string expression)
        {
            return AboutCommand(expression);
        }
    }
}
