using System.Web.Http;
using WebApiTier.ServiceClients;
using WebApiTier.UserServiceReference;

namespace WebApiTier.Controllers
{
    [RoutePrefix("api")]
    public class UserController : ApiController
    {
        [Authorize]
        [Route("users/{id}")]
        [HttpGet]
        public IHttpActionResult GetUserById(int id)
        {
            var user = UserClient.Instance.Client.GetUserById(id);
            if (user == null)
                return InternalServerError();
            return Ok(user);
        }

        [Authorize]
        [Route("users")]
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            var user = UserClient.Instance.Client.GetAllUsers();
            if (user == null)
                return InternalServerError();
            return Ok(user);
        }

        [Authorize]
        [Route("users/{id}/chats/")]
        [HttpGet]
        public IHttpActionResult GetChatsOfUser(int id)
        {
            var user = new UserInfoDTO()
            {
                Id = id
            };
            var chats = UserClient.Instance.Client.GetChatsOfUser(user);
            if (chats == null)
                return InternalServerError();
            return Ok(chats);
        }

        [Authorize]
        [Route("users/{id}/friends")]
        [HttpGet]
        public IHttpActionResult GetUserFriends(int id)
        {
            var user = new UserInfoDTO()
            {
                Id = id
            };
            var friends = UserClient.Instance.Client.GetUserFriends(user);
            if (friends == null)
                return InternalServerError();
            return Ok(friends);
        }

        [Authorize]
        [Route("users/{id1}/mutual/{id2}")]
        [HttpGet]
        public IHttpActionResult GetMutualFriends(int id1, int id2)
        {
            var user1 = new UserInfoDTO()
            {
                Id = id1
            };
            var user2 = new UserInfoDTO()
            {
                Id = id2
            };
            var friends = UserClient.Instance.Client.GetMutualFriends(user1, user2);
            if (friends == null)
                return InternalServerError();
            return Ok(friends);
        }

        [Authorize]
        [Route("users/{id1}/way/{id2}")]
        [HttpGet]
        public IHttpActionResult GetWayBetweenUsers(int id1, int id2)
        {
            var user1 = new UserInfoDTO()
            {
                Id = id1
            };
            var user2 = new UserInfoDTO()
            {
                Id = id2
            };
            var friends = UserClient.Instance.Client.GetWayBetweenUsers(user1, user2);
            if (friends == null)
                return InternalServerError();
            return Ok(friends);
        }

        [Authorize]
        [Route("users/{id}")]
        [HttpPost]
        public IHttpActionResult UpdateProfile([FromBody] UserInfoDTO dto, int id)
        {
            dto.Id = id;
            UserClient.Instance.Client.UpdateUser(dto);
            return Ok();
        }
    }
}