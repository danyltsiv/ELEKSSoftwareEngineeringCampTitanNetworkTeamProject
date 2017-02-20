using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicTier.DataTranferObjects;

namespace BusinessLogicTier.Services
{
    [ServiceContract]
    interface IChatService
    {
        // GET methods

        [OperationContract]
        ICollection<UserInfoDTO> GetAllUsersFromChat(int id);

        [OperationContract]
        ICollection<MessageDTO> GetAllMessagesFromChat(int id);

        // POST methods

        [OperationContract]
        void AddChat(ChatDTO chat);

        [OperationContract]
        void AddUser(int id, UserInfoDTO user);

        [OperationContract]
        void AddMessage(int id, MessageDTO message);

        // PUT methods

        [OperationContract]
        void UpdateChat(ChatDTO chat);
    }
}