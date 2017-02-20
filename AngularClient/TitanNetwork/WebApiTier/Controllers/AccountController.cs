using System.Web.Http;
using WebApiTier.ServiceClients;
using WebApiTier.UserServiceReference;

namespace WebApiTier.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        [AllowAnonymous]
        [Route("Register")]
        public IHttpActionResult Register(AccountDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultBool = UserClient.Instance.Client.AddAccount(dto);

            if (!resultBool)
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}