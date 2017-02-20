using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiTier.Helpers;
using WebApiTier.Helpers.ActionResults;
using WebApiTier.Providers;
using WebApiTier.UserServiceReference;

namespace WebApiTier.Controllers
{
    /// <summary>
    /// Class AccountController.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>IHttpActionResult.</returns>
        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public IHttpActionResult Register(AccountDTO dto)
        {
            Logger.log.Debug("at AccountController.Register");

            if (!ModelState.IsValid)
            {
                Logger.log.Error("at AccountController.Register - Bad model state");
                return BadRequest(ModelState);
            }

            OperationResultDTO result = null;
            using (var client = SoapProvider.GetUserServiceClient())
            {
                try
                {
                    if (client.FindUserByUserName(dto.UserName) != null)
                        return BadRequest("User with this name is loginned");

                    result = client.AddAccount(dto);
                    if (!result.Result)
                    {
                        Logger.log.Error("at AccountController.Register - " + result.Info);
                        return BadRequest((string)result.Info);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }
            
            return Ok((int)result.Info);
        }

        /// <summary>
        /// Get user by account name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>User info</returns>
        [Authorize]
        [HttpGet]
        [Route("{name}")]
        [ResponseType(typeof(UserInfoDTO))]
        public IHttpActionResult GetUserByAccountName(string name)
        {
            Logger.log.Debug("at AccountController.GetUserByAccountName");
            UserInfoDTO user = null;

            using (var client = SoapProvider.GetUserServiceClient())
            {
                user = client.GetAllUsers()
                    .AsQueryable()
                    .Where(item => item.UserName == name)
                    .ToList()
                    .Single();
            }

            if (user == null)
            {
                Logger.log.Error("at AccountController.GetUserByAccountName - Error at database");
                return BadRequest("User not found");
            }

            return Ok(user);
        }

        /// <summary>
        /// Delete user forever
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IHttpActionResult.</returns>
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteAccount(int id)
        {
            Logger.log.Debug("at AccountController.DeleteAccount");

            if (!this.CheckUserIdAcces(id))
            {
                Logger.log.Error("at AccountController.DeleteAccount - User request denied");
                return new Forbidden("User cannot modify other profile!");
            }

            using (var client = SoapProvider.GetUserServiceClient())
            {
                try
                {
                    var result = client.DeleteUser(id);
                    if (!result.Result)
                    {
                        Logger.log.Error("at AccountController.DeleteAccount - " + result.Info);
                        return BadRequest((string)result.Info);
                    }
                }
                catch (Exception ex)
	            {
                    return BadRequest(ex.ToString());
                }
            }

            return Ok();
        }
    }
}