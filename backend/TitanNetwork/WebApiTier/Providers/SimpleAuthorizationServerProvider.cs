using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiTier.Helpers;
using WebApiTier.UserServiceReference;

namespace WebApiTier.Providers
{
    /// <summary>
    /// Class SimpleAuthorizationServerProvider.
    /// </summary>
    /// <seealso cref="Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerProvider" />
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Called to validate that the origin of the request is a registered "client_id", and that the correct credentials for that client are
        /// present on the request. If the web application accepts Basic authentication credentials,
        /// context.TryGetBasicCredentials(out clientId, out clientSecret) may be called to acquire those values if present in the request header. If the web
        /// application accepts "client_id" and "client_secret" as form encoded POST parameters,
        /// context.TryGetFormCredentials(out clientId, out clientSecret) may be called to acquire those values if present in the request body.
        /// If context.Validated is not called the request will not proceed further.
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>Task to enable asynchronous execution</returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        /// <summary>
        /// Called when a request to the Token endpoint arrives with a "grant_type" of "password". This occurs when the user has provided name and password
        /// credentials directly into the client application's user interface, and the client application is using those to acquire an "access_token" and
        /// optional "refresh_token". If the web application supports the
        /// resource owner credentials grant type it must validate the context.Username and context.Password as appropriate. To issue an
        /// access token the context.Validated must be called with a new ticket containing the claims about the resource owner which should be associated
        /// with the access token. The application should take appropriate measures to ensure that the endpoint isn’t abused by malicious callers.
        /// The default behavior is to reject this grant type.
        /// See also http://tools.ietf.org/html/rfc6749#section-4.3.2
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>Task to enable asynchronous execution</returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            Logger.log.Debug("Loggin in in AuthorizationServerProvider");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var dto = new AccountDTO()
            {
                UserName = context.UserName,
                Password = context.Password,
                ConfirmPassword = context.Password
            };

            OperationResultDTO result = null;
            using (var client = SoapProvider.GetUserServiceClient())
            {
                result = client.CheckCredentials(dto);
            }
            if (!result.Result)
            {
                Logger.log.Error("The user name of password is incorrect");
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = ConfigureClaims(context, dto);
            context.Validated(identity);
        }

        /// <summary>
        /// Configures the claims.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="dto">The dto.</param>
        /// <returns>ClaimsIdentity.</returns>
        private ClaimsIdentity ConfigureClaims(OAuthGrantResourceOwnerCredentialsContext context, AccountDTO dto)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            using (var client = SoapProvider.GetUserServiceClient())
            {
                var user = client.FindUserByCredentials(dto);
                int createdChatId = 0;

                using (var botClient = SoapProvider.GetBotServiceClient())
                {
                    createdChatId = (int) botClient.AttachBotToUser(user.Id).Info;
                }

                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("Id", user.Id.ToString(), ClaimValueTypes.Integer));
                identity.AddClaim(new Claim("BotId", createdChatId.ToString(), ClaimValueTypes.Integer));
                identity.AddClaim(new Claim("role", "user"));
            }

            return identity;
        }       
    }
}