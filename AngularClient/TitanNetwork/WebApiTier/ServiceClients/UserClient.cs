using WebApiTier.UserServiceReference;

namespace WebApiTier.ServiceClients
{
    public class UserClient
    {
        private static readonly UserClient _instance = new UserClient();
        public UserServiceClient Client { get; private set; }

        private UserClient()
        {
            Client = new UserServiceClient();
        }

        public static UserClient Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}