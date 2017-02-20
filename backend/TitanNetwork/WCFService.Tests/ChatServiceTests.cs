using NUnit.Framework;
using BusinessLogicTier.Providers;
using WCFService.Tests.ChatServiceReference;

namespace WCFService.Tests
{
    [TestFixture]
    class ChatServiceTests
    {
        private ChatServiceReference.ChatServiceClient _chatClient;
        private UserServiceReference.UserServiceClient _userClient;
        private ChatProvider ChatProvider;

        public ChatServiceTests()
        {
            _chatClient = new ChatServiceReference.ChatServiceClient();
            _userClient = new UserServiceReference.UserServiceClient();
            ChatProvider = new ChatProvider();
        }

        [Test]
        public void WCFTestGetAllMessagesFromChat()
        {
            var messages = _chatClient.GetAllMessagesFromChat(1);
            Assert.That(messages[0].NewContent == "privet ya 1" && messages.Length == 3);
        }

        [Test]
        public void WCFTestGetAllUsersFromChat()
        {
            var users = _chatClient.GetAllUsersFromChat(1);
            Assert.That(users.Length == 3 && users[0].FirstName == "John");
        }

        [Test]
        public void WCFTestAddChat()
        {
            ChatDTO chat = new ChatDTO();
            int before = ChatProvider.GetAllChats().Count;
            _chatClient.AddChat(chat);
            var after = ChatProvider.GetAllChats().Count;
            Assert.That(after == before + 1); 
        }
        
        [Test]
        public void WCFTestUpdateChat()
        {
            ChatDTO chat = _chatClient.GetChatById(1);
            string before = chat.Title;
            chat.Title = chat.Title + "A";
            _chatClient.UpdateChat(chat);
            ChatDTO chatAfter = _chatClient.GetChatById(1);
            string after = chatAfter.Title;
            Assert.That(before != after);
        }

        [Test]
        public void WCFTestGetChatById()
        {
            var chat = _chatClient.GetChatById(3);
            Assert.That(chat.Title == "Chat");
        }
    }
}
