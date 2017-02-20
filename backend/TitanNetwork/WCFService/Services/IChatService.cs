using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using WCFService.DataTranferObjects;

namespace WCFService.Services
{
    [ServiceContract]
    interface IChatService
    {
        // GET methods

        [OperationContract]
        IList<UserInfoDTO> GetAllUsersFromChat(int id);

        [OperationContract]
        IList<MessageDTO> GetAllMessagesFromChat(int id);

        [OperationContract]
        ChatDTO GetChatById(int id);

        // POST methods

        [OperationContract]
        int AddChat(ChatDTO chat);

        [OperationContract]
        bool AddUser(int id, int userId);

        [OperationContract]
        int AddMessage(int id, MessageDTO message);

        // PUT methods

        [OperationContract]
        bool UpdateChat(ChatDTO chat);
    }
}