using System.Net.Mail;


namespace TitanWcfService.Services.Bots.Commands.Email
{
    public class EmailMessager : ICommander
    {
        private readonly string _msgFrom;
        private readonly string _msgPassword;

        private readonly string _userUrl;
        private readonly string _userFirstName;
        private readonly string _userSecondName;
        private const char Delimiter = '/';

        public EmailMessager(string userUrl, string userFirstName, string userSecondName)
        {
            var data = GetLoginAndPasswordOfTheEmail().Split(Delimiter);

            _msgFrom = data[0];
            _msgPassword = data[1];
            _userUrl = userUrl;
            _userFirstName = userFirstName;
            _userSecondName = userSecondName;
        }

        public string Send(string data)
        {

            var result = data.Split(Delimiter);

            switch (result.Length)
            {
                case 3:
                    return Send(result[0], result[1], result[2]);
                case 5:
                    return Send(result[0], result[1], result[2], result[3], result[4]);
            }
            return Constants.ResponseToTheWrongCommand.Emailer;
        }

        public string Send(string msgTo, string msgSubject, string msgBody)
        {
            const string enter = "\n";
            const string warning = "WARNING! This email message send by bot TCN.";
            const string responsible = "TCN is not responsible for the content of the e-mail.";

            var user = string.Concat("Content that contains the message was written by ", _userFirstName, " ", _userSecondName, ".");
            var url = string.Concat("Link : ", _userUrl);

            msgBody = string.Concat(warning, enter, user, enter, url, enter, enter, msgBody, enter, enter, responsible);

            return Send(_msgFrom, _msgPassword, msgTo, msgSubject, msgBody);
        }

        public string Send(string msgFrom, string msgPassword, string msgTo, string msgSubject, string msgBody)
        {

            if (string.IsNullOrEmpty(msgFrom)
                || string.IsNullOrEmpty(msgPassword)
                || string.IsNullOrEmpty(msgTo)
                || string.IsNullOrEmpty(msgSubject)
                || string.IsNullOrEmpty(msgBody))
            {
                return TitanWcfService.Constants.ResponseToTheWrongCommand.Emailer;
            }

            const char seperator = '@';
            var emailSplits = msgFrom.Split(seperator);
            var smtp = string.Concat("smtp.", emailSplits[emailSplits.Length - 1]);
            var mail = new MailMessage(msgFrom, msgTo, msgSubject, msgBody);
            var client = new SmtpClient(smtp)
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential(msgFrom, msgPassword),
                EnableSsl = true
            };
            try
            {
                client.Send(mail);
            }
            catch (SmtpException)
            {
                return Constants.ResponseToTheWrongCommand.Emailer;
            }
            var responce = string.Concat("The email to ", msgTo, " was sended access");
            return responce;
        }

        public string GetLoginAndPasswordOfTheEmail()
        {
            return "tcnbotemailsender@mail.ru/GZK7mCztA3nuZftadRtJuMcy";
        }

        public string Execute(string expression)
        {
            return Send(expression);
        }
    }
}
