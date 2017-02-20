using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BusinessLogicTier.DataTranferObjects;

namespace BusinessLogicTier.Services
{
    public class ChatService : IChatService
    {
        public void AddChat(ChatDTO chat)
        {
            throw new NotImplementedException();
        }

        public void AddMessage(int id, MessageDTO message)
        {
            throw new NotImplementedException();
        }

        public void AddUser(int id, UserInfoDTO user)
        {
            throw new NotImplementedException();
        }

        public ICollection<MessageDTO> GetAllMessagesFromChat(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<UserInfoDTO> GetAllUsersFromChat(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateChat(ChatDTO chat)
        {
            throw new NotImplementedException();
        }
    }
}
