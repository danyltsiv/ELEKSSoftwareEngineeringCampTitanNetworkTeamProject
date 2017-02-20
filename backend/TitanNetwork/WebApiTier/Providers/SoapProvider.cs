using WebApiTier.UserServiceReference;
using WebApiTier.ChatServiceReference;
using WebApiTier.BotServiceReference;

namespace WebApiTier.Providers
{
    /// <summary>
    /// Class SoapProvider.
    /// </summary>
    public class SoapProvider
    {
        /// <summary>
        /// Gets the user service client.
        /// </summary>
        /// <returns>UserServiceClient.</returns>
        public static UserServiceClient GetUserServiceClient()
        {
            return new UserServiceClient();
        }

        /// <summary>
        /// Gets the chat service client.
        /// </summary>
        /// <returns>ChatServiceClient.</returns>
        public static ChatServiceClient GetChatServiceClient()
        {
            return new ChatServiceClient();
        }

        /// <summary>
        /// Gets the bot service client.
        /// </summary>
        /// <returns>BotServiceClient.</returns>
        public static BotServiceClient GetBotServiceClient()
        {
            return new BotServiceClient();
        }
    }
}