using System.Collections.Generic;
using BusinessLogicTier.Providers;
using WCFService.DataTranferObjects;
using WCFService.EntityConverters;
using System.Linq;
using System;
using BusinessLogicTier.DataAccesLayer.Entities;
using BusinessLogicTier;

namespace WCFService.Services
{
    public class ChatService : IChatService
    {
        protected ChatProvider ChatProvider { get; private set; }
        protected MessageProvider MessageProvider { get; private set; }
        private UserConverter _userConverter;
        private ChatConverter _chatConverter;
        private MessageConverter _messageConverter;

        public ChatService()
        {
            ChatProvider = new ChatProvider();
            MessageProvider = new MessageProvider();
            _userConverter = new UserConverter();
            _chatConverter = new ChatConverter();
            _messageConverter = new MessageConverter();
        }

        /// <summary>
        /// Get specific chat
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ChatDTO</returns>
        public ChatDTO GetChatById(int id)
        {
            Logger.log.Debug("at WCFService.ChatService.GetChatById");
            var chat = ChatProvider.GetChatById(id);
            return _chatConverter.ToDataTransferEntity(chat);
        }

        /// <summary>
        /// Add chat to db
        /// </summary>
        /// <param name="newChat"></param>
        /// <returns>Created chat id</returns>
        public int AddChat(ChatDTO newChat)
        {
            Logger.log.Debug("at WCFService.ChatService.AddChat");
            var chat = _chatConverter.ToBusinessEntity(newChat);
            return ChatProvider.AddChat(chat);
        }

        /// <summary>
        /// Add message to chat
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="newMessage"></param>
        /// <returns>Created message id</returns>
        public int AddMessage(int chatId, MessageDTO newMessage)
        {
            Logger.log.Debug("at WCFService.ChatService.AddMessage");
            var message = _messageConverter.ToBusinessEntity(newMessage);
            message.ChatId = chatId;
            message.SendDate = DateTime.Now;
            Logger.log.Debug(message);
            return MessageProvider.AddMessage(message);
        }

        /// <summary>
        /// Add user to chat
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="userId"></param>
        /// <returns>Result of opreation</returns>
        public bool AddUser(int chatId, int userId)
        {
            Logger.log.Debug("at WCFService.ChatService.AddUser");
            var user = new User() { Id = userId };
            var chat = ChatProvider.GetChatById(chatId);
            return ChatProvider.AddUserToChat(user, chat);
        }

        /// <summary>
        /// Get all messages from this chat
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of messages</returns>
        public IList<MessageDTO> GetAllMessagesFromChat(int id)
        {
            Logger.log.Debug("at WCFService.ChatService.GetAllMessagesFromChat");
            var chat = ChatProvider.GetChatById(id);
            var messages = ChatProvider.GetMessagesFromChat(chat);
            return _messageConverter.ToDataTransferEntityList(messages.ToList());
        }

        /// <summary>
        /// Get all users from this chat
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of users</returns>
        public IList<UserInfoDTO> GetAllUsersFromChat(int id)
        {
            Logger.log.Debug("at WCFService.ChatService.GetAllUsersFromChat");
            var chat = ChatProvider.GetChatById(id);
            var users = ChatProvider.GetUsersOfChat(chat);
            return _userConverter.ToDataTransferEntityList(users.ToList());
        }

        /// <summary>
        /// Update this chat
        /// </summary>
        /// <param name="chat"></param>
        /// <returns>Result</returns>
        public bool UpdateChat(ChatDTO chat)
        {
            Logger.log.Debug("at WCFService.ChatService.UpdateChat");
            return ChatProvider.UpdateChat(_chatConverter.ToBusinessEntity(chat));
        }

        public virtual void Dispose()
        {
            ChatProvider.Dispose();
            MessageProvider.Dispose();
        }
    }
}
