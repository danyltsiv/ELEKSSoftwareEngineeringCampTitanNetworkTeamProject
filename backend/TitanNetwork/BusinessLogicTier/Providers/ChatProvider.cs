using BusinessLogicTier.DataAccesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BusinessLogicTier.Providers
{
    public class ChatProvider : BaseProvider
    {
        /// <summary>
        /// Get concrete chat from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Chat</returns>
        public Chat GetChatById(int id)
        {
            Logger.log.Debug("at BusinessLogicTier.Providers.GetChatById");
            return Context.Chats.FirstOrDefault(g => g.Id == id);
        }

        /// <summary>
        /// Get queryable to perform custom queries
        /// </summary>
        /// <returns>Chat queryable</returns>
        public IQueryable<Chat> GetChatQueryable()
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.GetChatById");
                return Context.Chats.AsQueryable();
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.GetChatById - " + ex.ToString());
                return null;
            }
        }

        public bool AddUserToChat(User user, Chat chat)
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.AddUserToChat");
                var room = new Room();
                room.UserId = user.Id;
                room.ChatId = chat.Id;
                Context.Rooms.Add(room);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.AddUserToChat - " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Delete from this chat
        /// </summary>
        /// <param name="user"></param>
        /// <param name="chat"></param>
        /// <returns>bool</returns>
        public bool RemoveUserFromChat(User user, Chat chat)
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.RemoveUsersFromChat");
                var usersOfChat = GetUsersOfChat(chat).ToList();

                if (usersOfChat.Count == 1)
                {
                    RemoveChat(chat);
                }
                else
                {
                    var room = Context.Rooms.FirstOrDefault(g => g.UserId == user.Id && g.ChatId == chat.Id);
                    Context.Entry(room).State = EntityState.Deleted;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.RemoveUserFromChat - " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Get all users that are in this chat
        /// </summary>
        /// <param name="chat"></param>
        /// <returns>Queryable list of users</returns>
        public IQueryable<User> GetUsersOfChat(Chat chat)
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.GetUsersOfChat");
                List<User> users;

                var userIds = Context.Rooms.Where(g => g.ChatId == chat.Id).Select(g => g.UserId).ToList();
                users = Context.Users.Where(g => userIds.Contains(g.Id
                    )).ToList();

                return users.AsQueryable();
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.GetUsersOfChat - " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get all messages from this chat
        /// </summary>
        /// <param name="chat"></param>
        /// <returns>Queryable list of messages</returns>
        public IQueryable<Message> GetMessagesFromChat(Chat chat)
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.GetMessagesFromChat");
                var chatMessages = Context.Messages.Where(g => g.ChatId == chat.Id);
                return chatMessages;
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.GetMessagesFromChat - " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Create new chat in database
        /// </summary>
        /// <param name="chat"></param>
        /// <returns>Created chat id or -1 otherwise</returns>
        public int AddChat(Chat chat)
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.AddChat");
                Context.Entry(chat).State = EntityState.Added;
                Context.SaveChanges();
                return Context.Chats.ToList().Last().Id;
            }
            catch(Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.AddChat - " + ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// Remove chat from database
        /// </summary>
        /// <param name="chat"></param>
        /// <returns>bool</returns>
        public bool RemoveChat(Chat chat)
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.RemoveChat");
                Context.Entry(chat).State = EntityState.Deleted;
                Context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.RemoveChat - " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Update chat content
        /// </summary>
        /// <param name="chat"></param>
        /// <returns>bool</returns>
        public bool UpdateChat(Chat chat)
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.UpdateChat");
                Context.Entry(chat).State = EntityState.Modified;
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.UpdateChat - " + ex.ToString());
                return false;
            }
        }
    }
}