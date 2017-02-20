using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using WebApiTier.ChatServiceReference;

namespace WebApiTier.Helpers
{
    /// <summary>
    /// Class CurrentUserProvider.
    /// </summary>
    public static class CurrentUserProvider
    {
        /// <summary>
        /// Gets the current user identifier.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>System.Int32.</returns>
        public static int GetCurrentUserId(this ApiController controller)
        {
            var principal = controller.RequestContext.Principal as ClaimsPrincipal;
            return int.Parse(principal.Claims.Single(c => c.Type == "Id").Value);
        }

        /// <summary>
        /// Creates the chat identifier from proxy.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="proxy">The proxy.</param>
        /// <returns>System.Int32.</returns>
        public static int CreateChatIdFromProxy(this ApiController controller, string proxy)
        {
            var principal = controller.RequestContext.Principal as ClaimsPrincipal;

            if (proxy == "bot")
                return int.Parse(principal.Claims.Single(c => c.Type == "BotId").Value);
            else
                return int.Parse(proxy);
        }

        /// <summary>
        /// Checks the user identifier acces.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CheckUserIdAcces(this ApiController controller, int userId)
        {
            return controller.GetCurrentUserId() == userId;
        }

        /// <summary>
        /// Ifs the user has acces to chat.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="client">The client.</param>
        /// <param name="chatId">The chat identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool IfUserHasAccesToChat(this ApiController controller, ChatServiceClient client, int chatId, int userId)
        {
            return client.GetAllUsersFromChat(chatId).ToList().Find(user => user.Id == userId) != null;
        }
    }
}