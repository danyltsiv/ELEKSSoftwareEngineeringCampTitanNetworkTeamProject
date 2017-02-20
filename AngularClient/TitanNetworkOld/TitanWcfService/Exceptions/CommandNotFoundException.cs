
namespace TitanWcfService.Services.Exceptions
{
    public class CommandNotFoundException : System.Exception
    {
        public CommandNotFoundException()
        {

        }

        public CommandNotFoundException(string message)
            : base(message)
        {

        }
    }
}
