using System;

namespace TitanWcfService.Services.Exceptions
{
    public class UserException : Exception
    {
        public string Msg { get; }

        public UserException(string s)
        {
            Msg = s;
        }
        public void What()
        {
            Console.WriteLine(Msg);
        }
    }
}