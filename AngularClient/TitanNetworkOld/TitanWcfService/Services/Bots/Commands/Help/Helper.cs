namespace TitanWcfService.Services.Bots.Commands.Help
{
    public class Helper : ICommander
    {
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

        public string Execute(string expression)
        {
            return AboutCommand(expression);
        }
    }
}
