using BusinessLogicTier.DataAccesLayer.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicTier.Providers.UserProviderInfrastructure
{
    public class UserProvider : AccountRepository
    {
        public UserProvider() 
            : base()
        {
            callback = new Callback(GetUserFriendsFromDB);
            RegisterCallback(callback);
        }

        private string GetUserFriendsFromDB(string userId)
        {
            List<User> userFriends = new List<User>();
            User user = GetUserById(int.Parse(userId));
            userFriends = GetUserFriends(user);
            string result = string.Join(" ", userFriends.Select(g => g.Id).ToArray());
            return result;
        }

        public bool UpdateUser(User user)
        {
            var result = UserManager.UpdateAsync(user);
            return result.IsCompleted;
        }

        public User GetUserById(int id)
        {
            try
            {
                var result = Context.Users.Where(user => user.Id == id).ToList().Single();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return UserManager.Users.ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<User> GetUserFriends(User user)
        {
            try
            {
                List<User> userFriends;              
                List<int> userChatIds = GetChatsOfUser(user).Select(g => g.Id).ToList();
                List<int> userIds = Context.Rooms.Where(g => userChatIds.Contains(g.ChatId)
                                    && g.UserId != user.Id).Select(g => g.UserId).ToList();
                userFriends = Context.Users.Where(g => userIds.Contains(g.Id)).ToList();

                return userFriends;
            }
            catch
            {
                return null;
            }
        }

        public override List<User> GetMutualFriends(User user1, User user2)
        {
            try
            {
                List<User> commonFriends;
                string common = GetCommonFriends(user1.Id.ToString(), user2.Id.ToString());
                List<string> commonIds = common.Split(' ').ToList();
                commonFriends = Context.Users.Where(g => commonIds.Contains(g.Id.ToString())).ToList();
                return commonFriends;
            }
            catch
            {
                return null;
            }
        }

        public override List<User> GetWayBetweenFriends(User user1, User user2)
        {
            try
            {
                List<User> wayUsers;
                string way = GetWayBetweenUsers(user1.Id.ToString(), user2.Id.ToString());
                List<string> wayIds = way.Split(' ').ToList();
                wayUsers = Context.Users.Where(g => wayIds.Contains(g.Id.ToString())).ToList();
                return wayUsers;
            }
            catch
            {
                return null;
            }
        }

        public List<Chat> GetChatsOfUser(User user)
        {
            try
            {
                List<Chat> userChats;
                userChats = Context.Chats.Where(g => Context.Rooms.Where(p => p.UserId == user.Id)
                .Select(p => p.ChatId)
                .Contains(g.Id))
                .Select(g => g).ToList();
                return userChats;
            }
            catch
            {
                return null;
            }
        }       
    }
}
