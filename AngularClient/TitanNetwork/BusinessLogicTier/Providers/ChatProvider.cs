using BusinessLogicTier.DataAccesLayer.Context;
using BusinessLogicTier.DataAccesLayer.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BusinessLogicTier.Providers
{
    public class ChatProvider
    {
        public ChatProvider()
        {
        }

        public Chat GetChatById(int id)
        {
            Chat chat;

            using (var context = new TitanNetworkContext())
            {
                chat = context.Chats.FirstOrDefault(g => g.Id == id);
            }

            return chat;
        }

        public bool AddUserToChat(User user, Chat chat)
        {
            try
            {
                using (var context = new TitanNetworkContext())
                {
                    Room room = new Room();
                    room.UserId = user.Id;
                    room.ChatId = chat.Id;
                    context.Rooms.Add(room);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveUserFromChat(User user, Chat chat)
        {
            try
            {
                List<User> usersOfChat = GetUsersOfChat(chat);

                if (usersOfChat.Count == 1)
                {
                    RemoveChat(chat);
                }
                else
                {
                    using (var context = new TitanNetworkContext())
                    {
                        Room room = context.Rooms.FirstOrDefault(g => g.UserId == user.Id && g.ChatId == chat.Id);
                        context.Entry(room).State = EntityState.Deleted;
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<User> GetUsersOfChat(Chat chat)
        {
            try
            {
                List<User> users;

                using (var context = new TitanNetworkContext())
                {
                    List<int> userIds = context.Rooms.Where(g => g.ChatId == chat.Id).Select(g => g.UserId).ToList();
                    users = context.Users.Where(g => userIds.Contains(g.Id
                        )).ToList();
                }

                return users;
            }
            catch
            {
                return null;
            }
        }

        public List<Message> GetMessagesFromChat(Chat chat)
        {
            try
            {
                List<Message> chatMessages;

                using (var context = new TitanNetworkContext())
                {
                    chatMessages = context.Messages.Where(g => g.ChatId == chat.Id).ToList();
                }

                return chatMessages;
            }
            catch
            {
                return null;
            }
        }

        public bool AddChat(Chat chat)
        {
            try
            {
                using (var context = new TitanNetworkContext())
                {
                    context.Entry(chat).State = EntityState.Added;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveChat(Chat chat)
        {
            try
            {
                using (var context = new TitanNetworkContext())
                {
                    context.Entry(chat).State = EntityState.Deleted;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateChat(Chat chat)
        {
            try
            {
                using (var context = new TitanNetworkContext())
                {
                    context.Entry(chat).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}