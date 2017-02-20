using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiTier.Helpers;
using WebApiTier.Helpers.ActionResults;
using WebApiTier.Helpers.Paging;
using WebApiTier.Providers;
using WebApiTier.UserServiceReference;
using static WebApiTier.Helpers.Paging.Pagination;

namespace WebApiTier.Controllers
{
    /// <summary>
    /// Class UserController.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {

        /// <summary>
        /// Get all users with applied paging
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>All users from page to page + pageSize</returns>
        [Authorize]
        [Route("", Name = "GetAllUsers")]
        [HttpGet]
        [ResponseType(typeof(List<UserInfoDTO>))]
        public IHttpActionResult GetAllUsers(int? page = null, int pageSize = 10)
        {
            Logger.log.Debug("at UserController.GetAllUsers");
            IQueryable<UserInfoDTO> user = null;
            using (var client = SoapProvider.GetUserServiceClient())
            {
                user = client.GetAllUsers().AsQueryable();
            }

            if (user == null)
            {
                Logger.log.Error("at UserController.GetAllUsers - Error at database");
                return InternalServerError();
            }
            
            var data = this.ApplyPaging(user, page.Value, pageSize, "GetAllUsers");
            return Ok(data);
        }

        /// <summary>
        /// Get info about specified user
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Info about user</returns>
        [Authorize]
        [Route("{id}")]
        [HttpGet]
        [ResponseType(typeof(UserInfoDTO))]
        public IHttpActionResult GetUserById(int id)
        {
            Logger.log.Debug("at UserController.GetUserById");
            UserInfoDTO user = null;
            using (var client = SoapProvider.GetUserServiceClient())
            {
                user = client.GetUserById(id);
            }

            if (user == null)
            {
                Logger.log.Error("at UserController.GetUserById - Error at database");
                return InternalServerError();
            }

            return Ok(user);
        }

        /// <summary>
        /// Get all friends of user
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>List of user infos</returns>
        [Authorize]
        [Route("{id}/friends", Name = "GetUserFriends")]
        [HttpGet]
        [ResponseType(typeof(List<UserInfoDTO>))]
        public IHttpActionResult GetUserFriends(int id, int? page = null, int pageSize = 10)
        {
            Logger.log.Debug("at UserController.GetUserFriends");
            IQueryable<UserInfoDTO> friends = null;
            
            using (var client = SoapProvider.GetUserServiceClient())
            {
                friends = SoapProvider.GetUserServiceClient().GetUserFriends(id).AsQueryable();
            }

            if (friends == null)
            {
                Logger.log.Error("at UserController.GetUserFriends - Error at database");
                return InternalServerError();
            }
            var data = this.ApplyPaging(friends, page.Value, pageSize, "GetUserFriends");
            return Ok(data);
        }

        /// <summary>
        /// Get mutual friends between two users
        /// </summary>
        /// <param name="id1">The id1.</param>
        /// <param name="id2">The id2.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>List of user infos</returns>
        [Authorize]
        [Route("{id1}/mutual/{id2}", Name = "GetMutualFriends")]
        [HttpGet]
        [ResponseType(typeof(List<UserInfoDTO>))]
        public IHttpActionResult GetMutualFriends(int id1, int id2, int? page = null, int pageSize = 10)
        {
            Logger.log.Debug("at UserController.GetMutualFriends");
            IQueryable<UserInfoDTO> friends = null;

            if (!this.CheckUserIdAcces(id1))
            {
                Logger.log.Error("at UserController.GetMutualFriends - User request denied");
                return new Forbidden("User cannot look at mutual friends of other!");
            }

            using (var client = SoapProvider.GetUserServiceClient())
            {
                friends = SoapProvider.GetUserServiceClient().GetMutualFriends(id1, id2).AsQueryable();
            }

            if (friends == null)
            {
                Logger.log.Error("at UserController.GetMutualFriends - Error at database");
                return InternalServerError();
            }
            var data = this.ApplyPaging(friends, page.Value, pageSize, "GetMutualFriends");
            return Ok(data);
        }

        /// <summary>
        /// Get way between two users
        /// </summary>
        /// <param name="id1">The id1.</param>
        /// <param name="id2">The id2.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>List of user infos</returns>
        [Authorize]
        [Route("{id1}/way/{id2}", Name = "GetWayBetweenUsers")]
        [HttpGet]
        [ResponseType(typeof(List<UserInfoDTO>))]
        public IHttpActionResult GetWayBetweenUsers(int id1, int id2, int? page = null, int pageSize = 10)
        {
            Logger.log.Debug("at UserController.GetWayBetweenUsers");
            IQueryable<UserInfoDTO> friends = null;

            if (!this.CheckUserIdAcces(id1))
            {
                Logger.log.Error("at UserController.GetWayBetweenUsers - User request denied");
                return new Forbidden("User cannot look at other friens way!");
            }

            using (var client = SoapProvider.GetUserServiceClient())
            {
                friends = SoapProvider.GetUserServiceClient().GetWayBetweenUsers(id1, id2).AsQueryable();
            }

            if (friends == null)
            {
                Logger.log.Error("at UserController.GetWayBetweenUsers - Error at database");
                return InternalServerError();
            }

            var data = this.ApplyPaging(friends, page.Value, pageSize, "GetWayBetweenUsers");
            return Ok(data);
        }

        /// <summary>
        /// Update the user info
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns>IHttpActionResult.</returns>
        [Authorize]
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult UpdateProfile(int id, [FromBody] UserInfoDTO dto)
        {
            Logger.log.Debug("at UserController.UpdateProfile");
            dto.Id = id;

            if (!this.CheckUserIdAcces(id))
            {
                Logger.log.Error("at UserController.UpdateProfile - User request denied");
                return new Forbidden("User cannot modify other profile!");
            }

            using (var client = SoapProvider.GetUserServiceClient())
            {
                try
                {
                    var result = client.UpdateUser(dto);
                    if (!result.Result)
                    {
                        Logger.log.Error("at UserController.UpdateProfile - " + result.Info);
                        return BadRequest((string)result.Info);
                    }
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }
            
            return Ok();
        }
    }
}