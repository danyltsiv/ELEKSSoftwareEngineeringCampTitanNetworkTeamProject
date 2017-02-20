namespace TitanWcfService.Services.Exceptions
{
    public class MessageException : System.Exception
    {
        public MessageException()
        {

        }

        public MessageException(string message)
            : base(message)
        {

        }
    }
}