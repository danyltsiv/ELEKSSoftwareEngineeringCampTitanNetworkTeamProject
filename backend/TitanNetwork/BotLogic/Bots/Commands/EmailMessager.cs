using System.Net.Mail;


namespace TitanWcfService.Services.Bots.Commands.Email
{
    /// <summary>
    /// Class EmailMessager.
    /// </summary>
    /// <seealso cref="TitanWcfService.Services.Bots.Commands.ICommander" />
    public class EmailMessager : ICommander
    {
        /// <summary>
        /// The _MSG from
        /// </summary>
        private readonly string _msgFrom;
        /// <summary>
        /// The _MSG password
        /// </summary>
        private readonly string _msgPassword;

        /// <summary>
        /// The _user URL
        /// </summary>
        private readonly string _userUrl;
        /// <summary>
        /// The _user first name
        /// </summary>
        private readonly string _userFirstName;
        /// <summary>
        /// The _user second name
        /// </summary>
        private readonly string _userSecondName;
        /// <summary>
        /// The delimiter
        /// </summary>
        private const char Delimiter = '/';

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessager"/> class.
        /// </summary>
        /// <param name="userUrl">The user URL.</param>
        /// <param name="userFirstName">First name of the user.</param>
        /// <param name="userSecondName">Name of the user second.</param>
        public EmailMessager(string userUrl, string userFirstName, string userSecondName)
        {
            var data = GetLoginAndPasswordOfTheEmail().Split(Delimiter);

            _msgFrom = data[0];
            _msgPassword = data[1];
            _userUrl = userUrl;
            _userFirstName = userFirstName;
            _userSecondName = userSecondName;
        }

        /// <summary>
        /// Sends the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.String.</returns>
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

        /// <summary>
        /// Sends the specified MSG to.
        /// </summary>
        /// <param name="msgTo">The MSG to.</param>
        /// <param name="msgSubject">The MSG subject.</param>
        /// <param name="msgBody">The MSG body.</param>
        /// <returns>System.String.</returns>
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

        /// <summary>
        /// Sends the specified MSG from.
        /// </summary>
        /// <param name="msgFrom">The MSG from.</param>
        /// <param name="msgPassword">The MSG password.</param>
        /// <param name="msgTo">The MSG to.</param>
        /// <param name="msgSubject">The MSG subject.</param>
        /// <param name="msgBody">The MSG body.</param>
        /// <returns>System.String.</returns>
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

        /// <summary>
        /// Gets the login and password of the email.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetLoginAndPasswordOfTheEmail()
        {
            return "tcnbotemailsender@mail.ru/GZK7mCztA3nuZftadRtJuMcy";
        }

        /// <summary>
        /// Executes the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>System.String.</returns>
        public string Execute(string expression)
        {
            return Send(expression);
        }
    }
}
