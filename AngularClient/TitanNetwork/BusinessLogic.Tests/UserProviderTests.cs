using BusinessLogicTier.DataAccesLayer.Entities;
using BusinessLogicTier.Providers;
using BusinessLogicTier.Providers.UserProviderInfrastructure;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicTest
{
    [TestFixture]
    static class UserProviderTests
    {
        public static UserProvider usrPrv = new UserProvider();

        [Test]
        public static void IsGetUserByIdWokrs()
        {
            User User = usrPrv.GetUserById(4);
            Assert.That(User.FirstName == "Petro");
        }

        [Test]
        public static void IsGetAllUsersWorks()
        {
            List<User> usrs = usrPrv.GetAllUsers();
            int Count = usrPrv.GetAllUsers().Count;
            Assert.That(Count != 0);
        }

        [Test]
        public static void IsGetUserFriendsWorks()
        {
            var user = usrPrv.GetUserById(1);
            var friends = usrPrv.GetUserFriends(user);
            Assert.That(friends.Count == 2);
        }

        [Test]
        public static void IsGetMutualFriendsWorks()
        {
            var user = usrPrv.GetUserById(1);
            var user2 = usrPrv.GetUserById(2);
            var mutual = usrPrv.GetMutualFriends(user, user2);
            Assert.That(mutual[0].FirstName == "Pro100");
        }

        [Test]
        public static void IsGetWayBetweenFriendsWorks()
        {
            var user = usrPrv.GetUserById(1);
            var user2 = usrPrv.GetUserById(5);
            var way = usrPrv.GetWayBetweenFriends(user, user2);
            Assert.That(way[0].FirstName == "John" &&
                                way[1].FirstName == "Pro100" &&
                                way[2].FirstName == "Vasiok");
        }

        [Test]
        public static void IsGetChatOfUserWorks()
        {
            User User = usrPrv.GetUserById(3);
            List<Chat> chats = usrPrv.GetChatsOfUser(User);
            Assert.That(chats.Count == 2);
        }

        [Test]
        public static void IsUpdateWorks()
        {
            User userToUpdate = usrPrv.GetUserById(2);
            userToUpdate.FirstName = "NewName";
            usrPrv.UpdateUser(userToUpdate);
            User UpdatedUser = usrPrv.GetUserById(2);
            Assert.That(UpdatedUser.FirstName == "NewName");
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
