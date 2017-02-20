using BusinessLogicTier.DataAccesLayer.Entities;
using BusinessLogicTier.Providers.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicTier.Providers.UserProviderInfrastructure
{
    /// <summary>
    /// Implementation of all user functionality
    /// </summary>
    public class UserProvider : AccountRepository
    {
        public UserProvider() 
            : base()
        {
            /*callback = new Callback(GetUserFriendsFromDB);
            RegisterCallback(callback);*/
        }

        private string GetUserFriendsFromDB(string userId)
        {
            var userFriends = new List<User>();
            var user = GetUserById(int.Parse(userId));
            userFriends = GetUserFriends(user).ToList();
            string result = string.Join(" ", userFriends.Select(g => g.Id).ToArray());
            return result;
        }

        /// <summary>
        /// Update user info
        /// </summary>
        /// <param name="user"></param>
        /// <returns>OR that contains string message</returns>
        public OperationResult UpdateUser(User user)
        {
            Logger.log.Debug("at BusinessLogicTier.Providers.UpdateUser");

            var oldUser = GetUserById(user.Id);
            if (oldUser == null)
            {
                return new OperationResult()
                {
                    Result = false,
                    Info = "User not found"
                };
            }

            try
            {
                oldUser.FirstName = user.FirstName;
                oldUser.MidleName = user.MidleName;
                oldUser.LastName = user.LastName;
                oldUser.About = user.About;
                oldUser.Age = user.Age;
                oldUser.UserName = user.UserName;
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new OperationResult()
                {
                    Result = false,
                    Info = ex.ToString()
                };
            }
            return new OperationResult()
            {
                Result = true,
                Info = ""
            };
        }

        /// <summary>
        /// Return user instance with specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User instance on succes, null otherwise</returns>
        public User GetUserById(int id)
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.GetUserById");
                var result = UserManager.FindById(id);
                return result;
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.GetUserById - " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get queryable to perform custom queries
        /// </summary>
        /// <returns>All users queryable</returns>
        public IQueryable<User> GetUserQueryable()
        {
            Logger.log.Debug("at BusinessLogicTier.Providers.GetAllUsers");
            return UserManager.Users;
        }
      
        /// <summary>
        /// Get friends of specified user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Queryable list of users to perform custom queries</returns>
        public IQueryable<User> GetUserFriends(User user)
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.GetUserFriends");
                List<User> userFriends;              
                var userChatIds = GetChatsOfUser(user).Select(g => g.Id).ToList();
                var userIds = Context.Rooms.Where(g => userChatIds.Contains(g.ChatId)
                                    && g.UserId != user.Id).Select(g => g.UserId).ToList();
                userFriends = Context.Users.Where(g => userIds.Contains(g.Id)).ToList();

                return userFriends.AsQueryable();
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.GetUserFriends - " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get mutual friedns between two users
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        /// <returns>Queryable list of users</returns>
        public override IQueryable<User> GetMutualFriends(User user1, User user2)
        {
            try
            {
                /*var common = GetCommonFriends(user1.Id.ToString(), user2.Id.ToString());
                var commonIds = common.Split(' ').ToList();
                return Context.Users.Where(g => commonIds.Contains(g.Id.ToString()));*/
                Logger.log.Debug("at BusinessLogicTier.Providers.GetMutualFriends");
                var friendList1 = GetUserFriends(user1);
                var friendList2 = GetUserFriends(user2);
                return       from friend1 in friendList1
                             join friend2 in friendList2
                             on friend1.Id equals friend2.Id
                             select friend1;
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.GetMutualFriends - " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get way from one user to another
        /// </summary>
        /// <param name="user1Dto"></param>
        /// <param name="user2Dto"></param>
        /// <returns>Queryable list of users that have mutual frieds</returns>
        public override IQueryable<User> GetWayBetweenFriends(User user1Dto, User user2Dto)
        {
            try
            {
                /*var way = GetWayBetweenUsers(user1.Id.ToString(), user2.Id.ToString());
                var wayIds = way.Split(' ').ToList();
                return Context.Users.Where(g => wayIds.Contains(g.Id.ToString()));*/
                var user1 = GetUserById(user1Dto.Id);
                var user2 = GetUserById(user2Dto.Id);
                if (user1.Id == user2.Id)
                {
                    var list = new List<User>();
                    list.Add(user1);
                    return list.AsQueryable();
                }

                var result = new List<User>();
                var userFriends1 = GetUserFriends(user1);
                var userFriends2 = GetUserFriends(user2);
                if (userFriends1.Count() < userFriends2.Count())
                {
                    if (userFriends1.Contains(user2))
                    {
                        var list = new List<User>();
                        list.Add(user1);
                        list.Add(user2);
                        return list.AsQueryable();
                    }
                }
                else
                {
                    if (userFriends2.Contains(user1))
                    {
                        var list = new List<User>();
                        list.Add(user1);
                        list.Add(user2);
                        return list.AsQueryable();
                    }
                }

                result.Add(user1);
                while (true)
                {
                    userFriends1 = GetUserFriends(user1);
                    if (userFriends1.Contains(user2))
                    {
                        result.Add(user2);
                        return result.AsQueryable();
                    }
                    foreach (var friend in userFriends1)
                    {
                        if (userFriends2.Contains(friend))
                        {
                            result.Add(friend);
                            result.Add(user2);
                            return result.AsQueryable();
                        }
                    }
                    var rand = new Random();
                    user1 = userFriends1.ToList()[rand.Next() % userFriends1.Count()];
                    result.Add(user1);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get all chats, to which this user has acces
        /// </summary>
        /// <param name="user"></param>
        /// <returns>List of chats</returns>
        public IQueryable<Chat> GetChatsOfUser(User user)
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.GetChatsOfUser");
                var userChats = Context.Chats.Where(g => Context.Rooms.Where(p => p.UserId == user.Id)
                .Select(p => p.ChatId)
                .Contains(g.Id))
                .Select(g => g);
                return userChats;
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.GetChatsOfUser - " + ex.ToString());
                return null;
            }
        } 
    }
}
