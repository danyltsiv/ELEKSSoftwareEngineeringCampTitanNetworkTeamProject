using System.Collections.Generic;
using System.Linq;



namespace TitanWcfService.DataAccesLayer.Services
{
    public class UserService
    {
        private TitanWcfService.DataAccesLayer.Context.TitanNetworkContext context;

        public UserService()
        {
            context = new TitanWcfService.DataAccesLayer.Context.TitanNetworkContext();
        }

        public void AddUser(TitanWcfService.DataAccesLayer.Entities.User user)
        {
            context.Users.Add(user);
        }

        public void RemoveUser(TitanWcfService.DataAccesLayer.Entities.User user)
        {
            context.Users.Remove(user);
        }

        public TitanWcfService.DataAccesLayer.Entities.User GetUserById(int id)
        {
            TitanWcfService.DataAccesLayer.Entities.User user = context.Users.FirstOrDefault(g => g.Id == id);

            return user;
        }

        public List<TitanWcfService.DataAccesLayer.Entities.User> GetAllUsers()
        {
            return context.Users.ToList();
        }

        public List<TitanWcfService.DataAccesLayer.Entities.User> GetUserFriends(TitanWcfService.DataAccesLayer.Entities.User user)
        {
            List<int> userChatIds = GetChatsOfUser(user).Select(g => g.Id).ToList();
            List<int> userIds = context.Rooms.Where(g => userChatIds.Contains(g.ChatId)
            && g.UserId != user.Id).Select(g => g.UserId).ToList();
            List<TitanWcfService.DataAccesLayer.Entities.User> userFriends = context.Users.Where(g => userIds.Contains(g.Id)).ToList();

            return userFriends;
        }

        public List<TitanWcfService.DataAccesLayer.Entities.User> GetMutualFriends(TitanWcfService.DataAccesLayer.Entities.User user1, TitanWcfService.DataAccesLayer.Entities.User user2)
        {
            List<TitanWcfService.DataAccesLayer.Entities.User> user1Friends = GetUserFriends(user1);
            List<TitanWcfService.DataAccesLayer.Entities.User> user2Friends = GetUserFriends(user2);
            List<TitanWcfService.DataAccesLayer.Entities.User> mutualFriends = user1Friends.Where(g => user2Friends.Contains(g)).ToList();

            return mutualFriends;
        }

        public List<TitanWcfService.DataAccesLayer.Entities.Chat> GetChatsOfUser(TitanWcfService.DataAccesLayer.Entities.User user)
        {
            List<TitanWcfService.DataAccesLayer.Entities.Chat> userChats = context.Chats.Where(g =>
            context.Rooms.Where(p => p.UserId == user.Id).Select(p => p.ChatId).Contains(g.Id)
            ).Select(g => g).ToList();

            return userChats;
        }
    }
}
