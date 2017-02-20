using System;
using System.Collections.Generic;
using BusinessLogicTier.DataAccesLayer.Entities;
using BusinessLogicTier.Providers;
using NUnit.Framework;
using BusinessLogicTier.Providers.UserProviderInfrastructure;

namespace BusinessLogicTest
{
    [TestFixture]
    static class ChatProviderTests
    {
        public static ChatProvider chatPrv = new ChatProvider();
        public static UserProvider usrPrv = new UserProvider();

        [Test]
        public static void IsGetChatByIdWorks()
        {
            Chat chat = chatPrv.GetChatById(1);
            Assert.That(chat.Title=="Chatik");
        }

        [Test]
        public static void IsGetUsersOfChatWorks()
        {
            Chat chat = chatPrv.GetChatById(1);
            List<User> users = chatPrv.GetUsersOfChat(chat);
            Assert.That(users.Count == 3);
        }

        [Test]
        public static void IsAddUserToChatWorks()
        {
            var user = usrPrv.GetUserById(1);
            var chat = chatPrv.GetChatById(2);
            int usersBefore = chatPrv.GetUsersOfChat(chat).Count;
            chatPrv.AddUserToChat(user, chat);
            int usersAfter = chatPrv.GetUsersOfChat(chat).Count;
            Assert.That(usersAfter == usersBefore + 1);
        }

        [Test]
        public static void IsGetMessagesFromChatWorks()
        {
            Chat chat = chatPrv.GetChatById(2);
            int messagesCount = chatPrv.GetMessagesFromChat(chat).Count;
            Assert.That(messagesCount == 3);
        }

        [Test]
        public static void IsRemoveUserFromChatWorks()
        {
            var user = usrPrv.GetUserById(1);
            var chat = chatPrv.GetChatById(2);
            int usersBefore = chatPrv.GetUsersOfChat(chat).Count;
            chatPrv.RemoveUserFromChat(user, chat);
            int usersAfter = chatPrv.GetUsersOfChat(chat).Count;
            Assert.That(usersAfter == usersBefore - 1);
        }
    }
}
