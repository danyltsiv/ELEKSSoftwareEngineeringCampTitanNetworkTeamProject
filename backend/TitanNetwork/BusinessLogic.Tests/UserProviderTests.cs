using BusinessLogicTier.DataAccesLayer.Entities;
using BusinessLogicTier.Providers.UserProviderInfrastructure;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicTest
{
    [TestFixture]
    static class UserProviderTests
    {
        public static UserProvider userProvider = new UserProvider();

        [Test]
        public static void IsGetUserByIdWorking()
        {
            User User = userProvider.GetUserById(4);
            Assert.That(User.FirstName == "Petro");
        }

        [Test]
        public static void IsGetAllUsersWorking()
        {
            var usrs = userProvider.GetUserQueryable();
            int Count = userProvider.GetUserQueryable().ToList().Count;
            Assert.That(Count != 0);
        }

        [Test]
        public static void IsGetUserFriendsWorking()
        {
            var user = userProvider.GetUserById(1);
            var friends = userProvider.GetUserFriends(user).ToList();
            Assert.That(friends.Count == 2);
        }

        [Test]
        public static void IsGetMutualFriendsWorking()
        {
            /*var user = userProvider.GetUserById(1);
            var user2 = userProvider.GetUserById(2);
            var mutual = userProvider.GetMutualFriends(user, user2);
            Assert.That(mutual[0].FirstName == "Pro100");*/
        }

        [Test]
        public static void IsGetWayBetweenFriendsWorks()
        {
           /* var user = userProvider.GetUserById(1);
            var user2 = userProvider.GetUserById(5);
            var way = userProvider.GetWayBetweenFriends(user, user2);
            Assert.That(way[0].FirstName == "John" &&
                                way[1].FirstName == "Pro100" &&
                                way[2].FirstName == "Vasiok");*/
        }

        [Test]
        public static void IsAddUserWorks()
        {
            /*User newUser = new User();
            int Before = usrPrv.GetAllUsers().Count;
            usrPrv.AddUser(newUser);
            int After = usrPrv.GetAllUsers().Count;
            Assert.That(Before == After - 1);*/
        }

        [Test]
        public static void IsGetChatOfUserWorks()
        {
            var user = userProvider.GetUserById(3);
            var chats = userProvider.GetChatsOfUser(user).ToList();
            Assert.That(chats.Count == 2);
        }

        [Test]
        public static void IsUpdateWorks()
        {
            /*User userToUpdate = usrPrv.GetUserById(2);
            userToUpdate.FirstName = "NewName";
            usrPrv.UpdateUser(userToUpdate);
            User UpdatedUser = usrPrv.GetUserById(2);
            Assert.That(UpdatedUser.FirstName == "NewName");
            */
        }

        [Test]
        public static void IsRemoveUserWorks()
        {
            /*int id = usrPrv.GetAllUsers().Select(g => g.Id).Max();
            User userToRemove = usrPrv.GetUserById(id);
            int Before = usrPrv.GetAllUsers().Count;
            usrPrv.RemoveUser(userToRemove);
            int After = usrPrv.GetAllUsers().Count;
            Assert.That(Before == After + 1);*/
        }
    }
}
