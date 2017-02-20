using System;
using NUnit.Framework;

namespace BusinessLogicTier.Tests
{
    [TestFixture]
    public class UserServiceTest
    {
        private UserServiceReference.UserServiceClient _client;

        public UserServiceTest()
        {
            _client = new UserServiceReference.UserServiceClient();
        }

        [Test]
        public void TestGetAllUsers()
        {
            var allUsers = _client.GetAllUsers();
            foreach (var user in allUsers)
            {
                Console.WriteLine(user.LastName);
            }
        }

        [Test]
        public void TestGetUser_2()
        {
            var user = _client.GetUserById(4);
            Assert.That(user.FirstName == "Petro");
        }

        [Test]
        public void TestComplexUsage()

        {
            var user = _client.GetUserById(1);
            user.LastName = "Petro";
            _client.UpdateUser(user);
            var friends = _client.GetUserFriends(1);
            Console.WriteLine(user.FirstName);
            foreach (var friend in friends)
            {
                Console.WriteLine(friend.MidleName);
            }
        }
    }
}
