using System.Collections.Generic;
using System.Linq;



namespace TitanWcfService.DataAccesLayer.Services
{
    public class ChatService
    {
        private TitanWcfService.DataAccesLayer.Context.TitanNetworkContext context;

        public ChatService()
        {
            context = new TitanWcfService.DataAccesLayer.Context.TitanNetworkContext();
        }

        public void AddUserToChat(TitanWcfService.DataAccesLayer.Entities.User user, TitanWcfService.DataAccesLayer.Entities.Chat chat)
        {
            TitanWcfService.DataAccesLayer.Entities.Room room = new TitanWcfService.DataAccesLayer.Entities.Room();
            room.UserId = user.Id;
            room.ChatId = chat.Id;
            context.Rooms.Add(room);
        }

        public void RemoveUserFromChat(TitanWcfService.DataAccesLayer.Entities.User user, TitanWcfService.DataAccesLayer.Entities.Chat chat)
        {
            List<TitanWcfService.DataAccesLayer.Entities.User> usersOfChat = GetUsersOfChat(chat);
            
            if(usersOfChat.Count==1)
            {
                RemoveChat(chat);
            }
            else
            {
                TitanWcfService.DataAccesLayer.Entities.Room room = context.Rooms.FirstOrDefault(g => g.UserId == user.Id && g.ChatId == chat.Id);
                context.Rooms.Remove(room);
            }
        }

        public List<TitanWcfService.DataAccesLayer.Entities.User> GetUsersOfChat(TitanWcfService.DataAccesLayer.Entities.Chat chat)
        {
            List<int> userIds = context.Rooms.Where(g => g.ChatId == chat.Id).Select(g => g.UserId).ToList();
            List<TitanWcfService.DataAccesLayer.Entities.User> users = context.Users.Where(g => userIds.Contains(g.Id)).ToList();

            return users;
        }

        public List<TitanWcfService.DataAccesLayer.Entities.Message> GetMessagesFromChat(TitanWcfService.DataAccesLayer.Entities.Chat chat)
        {
            List<TitanWcfService.DataAccesLayer.Entities.Message> chatMessages = context.Messages.Where(g => g.ChatId == chat.Id).ToList();

            return chatMessages;
        }

        public void AddChat(TitanWcfService.DataAccesLayer.Entities.Chat chat)
        {
            context.Chats.Add(chat);
        }

        public void RemoveChat(TitanWcfService.DataAccesLayer.Entities.Chat chat)
        {
            context.Chats.Remove(chat);
        }
    }
}
