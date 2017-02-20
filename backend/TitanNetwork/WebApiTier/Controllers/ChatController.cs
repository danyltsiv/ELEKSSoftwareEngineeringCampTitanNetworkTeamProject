using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiTier.ChatServiceReference;
using WebApiTier.Helpers;
using WebApiTier.Helpers.ActionResults;
using WebApiTier.Helpers.Paging;
using WebApiTier.Providers;

namespace WebApiTier.Controllers
{
    /// <summary>
    /// Class ChatController.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/users")]
    public class ChatController : ApiController
    {
        /// <summary>
        /// Returns messages from chat with applied paging
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="chat">The chat.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>List of messages</returns>
        [Authorize]
        [Route("{userId}/chats/{chat}/messages", Name = "GetAllMessagesFromChat")]
        [HttpGet]
        [ResponseType(typeof(MessageDTO))]
        public IHttpActionResult GetAllMessagesFromChat(int userId, string chat, int? page = null, int pageSize = 10)
        {
            Logger.log.Debug("at ChatController.GetAllMessagesFromChat");
            IQueryable<MessageDTO> messages = null;

            if (!this.CheckUserIdAcces(userId))
            {
                Logger.log.Error("at ChatController.GetAllMessagesFromChat - User request denied");
                return new Forbidden("User cannot look at other messages!");
            }

            int chatId = this.CreateChatIdFromProxy(chat);
            using (var client = SoapProvider.GetChatServiceClient())
            {
                if (!this.IfUserHasAccesToChat(client, chatId, userId))
                {
                    Logger.log.Error("at ChatController.GetChatById - User request denied");
                    return new Forbidden("User cannot have acces to other chat!");
                }

                messages = client.GetAllMessagesFromChat(chatId).AsQueryable();
            }

            if (messages == null)
            {
                Logger.log.Error("at ChatController.GetAllMessagesFromChat - Error at database");
                return InternalServerError();
            }

            var data = this.ApplyPaging(messages, page.Value, pageSize, "GetAllMessagesFromChat");
            return Ok(data);
        }

        /// <summary>
        /// Return all users that are in this chat
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="chat">The chat.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>List of chat infos</returns>
        [Authorize]
        [Route("{userId}/chats/{chat}/users", Name = "GetAllUsersFromChat")]
        [HttpGet]
        [ResponseType(typeof(UserInfoDTO))]
        public IHttpActionResult GetAllUsersFromChat(int userId, string chat, int? page = null, int pageSize = 10)
        {
            Logger.log.Debug("at ChatController.GetAllUsersFromChat");
            IQueryable<UserInfoDTO> users = null;

            if (!this.CheckUserIdAcces(userId))
            {
                Logger.log.Error("at ChatController.GetAllUsersFromChat - User request denied");
                return new Forbidden("User cannot have acces to other chats!");
            }

            int chatId = this.CreateChatIdFromProxy(chat);
            using (var client = SoapProvider.GetChatServiceClient())
            {
                if (!this.IfUserHasAccesToChat(client, chatId, userId))
                {
                    Logger.log.Error("at ChatController.GetChatById - User request denied");
                    return new Forbidden("User cannot have acces to other chat!");
                }

                users = client.GetAllUsersFromChat(chatId).AsQueryable();
            }

            if (users == null)
            {
                Logger.log.Error("at ChatController.GetAllUsersFromChat - Error at database");
                return InternalServerError();
            }

            var data = this.ApplyPaging(users, page.Value, pageSize, "GetAllUsersFromChat");
            return Ok(data);
        }

        /// <summary>
        /// Get concrete chat by id
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="chat">The chat.</param>
        /// <returns>Chat info</returns>
        [Authorize]
        [Route("{userId}/chats/{chat}")]
        [HttpGet]
        public IHttpActionResult GetChatById(int userId, string chat)
        {
            Logger.log.Debug("at ChatController.GetChatById");
            ChatDTO chatDTO = null;

            if (!this.CheckUserIdAcces(userId))
            {
                Logger.log.Error("at ChatController.GetChatById - User request denied");
                return new Forbidden("User cannot have acces to other chat!");
            }
            
            int chatId = this.CreateChatIdFromProxy(chat);
            using (var client = SoapProvider.GetChatServiceClient())
            {
                if (!this.IfUserHasAccesToChat(client, chatId, userId))
                {
                    Logger.log.Error("at ChatController.GetChatById - User request denied");
                    return new Forbidden("User cannot have acces to other chat!");
                }

                chatDTO = client.GetChatById(chatId);
            }

            if (chatDTO == null)
            {
                Logger.log.Error("at ChatController.GetChatById - Error at database");
                return InternalServerError();
            }

            return Ok(chatDTO);
        }

        /// <summary>
        /// Adds chat and attaches current user to it
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="chat">The chat.</param>
        /// <returns>Created chat id</returns>
        [Authorize]
        [Route("{userId}/chats")]
        [HttpPost]
        public IHttpActionResult AddChat(int userId, ChatDTO chat)
        {
            Logger.log.Debug("at ChatController.AddChat");
            int createdChatId;

            if (!this.CheckUserIdAcces(userId))
            {
                Logger.log.Error("at ChatController.AddChat - User request denied");
                return new Forbidden("User cannot add chat to other people!");
            }

            using (var client = SoapProvider.GetChatServiceClient())
            {
                createdChatId = client.AddChat(chat);
                if (createdChatId == -1)
                {
                    Logger.log.Error("at ChatController.AddChat - Error at database");
                    return InternalServerError();
                }

                if (!client.AddUser(createdChatId, userId))
                {
                    Logger.log.Error("at ChatController.AddChat - Error at database");
                    return InternalServerError();
                }
            }
            
            return Ok(createdChatId);
        }

        /// <summary>
        /// Send message to chat
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="chat">The chat.</param>
        /// <param name="message">The message.</param>
        /// <returns>Created message id</returns>
        [Authorize]
        [Route("{userId}/chats/{chat}/messages")]
        [HttpPost]
        public IHttpActionResult AddMessage(int userId, string chat, MessageDTO message)
        {
            Logger.log.Debug("at ChatController.AddMessage");
            int createdMessageId;

            if (!this.CheckUserIdAcces(userId))
            {
                Logger.log.Error("at ChatController.AddMessage - User request denied");
                return new Forbidden("User cannot send message from other accounts!");
            }

            if (userId != message.UserId)
            {
                Logger.log.Error("at ChatController.AddMessage - User request denied");
                return new Forbidden("User cannot send message from other accounts!");
            }

            int chatId = this.CreateChatIdFromProxy(chat);
            using (var client = SoapProvider.GetChatServiceClient())
            {
                if (!this.IfUserHasAccesToChat(client, chatId, userId))
                {
                    Logger.log.Error("at ChatController.GetChatById - User request denied");
                    return new Forbidden("User cannot have acces to other chat!");
                }

                createdMessageId = client.AddMessage(chatId, message);
                if (createdMessageId == -1)
                {
                    Logger.log.Error("at ChatController.AddMessage - Error at database");
                    return InternalServerError();
                }

                if (chat == "bot")
                {
                    using (var botClient = SoapProvider.GetBotServiceClient())
                    {
                        var messageToBot = new BotServiceReference.MessageDTO()
                        {
                            NewContent = message.NewContent,
                            UserId = message.UserId
                        };
                        botClient.SendMessageToBot(messageToBot, chatId);
                    }
                }
            }

            return Ok(createdMessageId);
        }

        /// <summary>
        /// Invite other user to chat
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="chat">The chat.</param>
        /// <param name="newUserId">The new user identifier.</param>
        /// <returns>IHttpActionResult.</returns>
        [Authorize]
        [Route("{userId}/chats/{chat}/users/{newUserId}")]
        [HttpPost]
        public IHttpActionResult AddUserToChat(int userId, string chat, int newUserId)
        {
            Logger.log.Debug("at ChatController.AddUserToChat");

            if (userId == newUserId)
            {
                Logger.log.Error("at ChatController.AddUserToChat - User cannot add himself to chat");
                return BadRequest("User cannot add himself to chat");
            }

            if (!this.CheckUserIdAcces(userId))
            {
                Logger.log.Error("at ChatController.AddUserToChat - User request denied");
                return new Forbidden("User has no acces!");
            }

            //if (userdId == SoapProvider.GetBotServiceClient())

            int chatId = 0;
            if (!int.TryParse(chat, out chatId))
            {
                return BadRequest("Bad chat id, or cannot add user to chat with a bot");
            }
                
            using (var client = SoapProvider.GetChatServiceClient())
            {
                if (!this.IfUserHasAccesToChat(client, chatId, userId))
                {
                    Logger.log.Error("at ChatController.GetChatById - User request denied");
                    return new Forbidden("User cannot have acces to other chat!");
                }

                if (!client.AddUser(chatId, newUserId))
                {
                    Logger.log.Error("at ChatController.AddUserToChat - Error at database");
                    return InternalServerError();
                }
            }

            return Ok();
        }

        /// <summary>
        /// Get all chats of current user
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>List of chat infos</returns>
        [Authorize]
        [Route("{id}/chats", Name = "GetChatsOfUser")]
        [HttpGet]
        [ResponseType(typeof(List<ChatDTO>))]
        public IHttpActionResult GetChatsOfUser(int id, int? page = null, int pageSize = 10)
        {
            Logger.log.Debug("at ChatController.GetChatsOfUser");
            IQueryable<UserServiceReference.ChatDTO> chats = null;

            if (!this.CheckUserIdAcces(id))
            {
                Logger.log.Error("at ChatController.GetChatsOfUser - User request denied");
                return new Forbidden("User cannot look at other chats!");
            }

            using (var client = SoapProvider.GetUserServiceClient())
            {
                chats = client.GetChatsOfUser(id).AsQueryable();
            }

            if (chats == null)
            {
                Logger.log.Error("at ChatController.GetChatsOfUser - Error at database");
                return InternalServerError();
            }

            var data = this.ApplyPaging(chats, page.Value, pageSize, "GetChatsOfUser");
            return Ok(chats);
        }
    }
}
