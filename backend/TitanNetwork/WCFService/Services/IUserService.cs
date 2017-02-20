using WCFService.DataTranferObjects;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using WCFService.DataTransferObjects;

namespace WCFService.Services
{
    [ServiceContract]
    public interface IUserService
    {
        // GET methods

        [OperationContract]
        IList<UserInfoDTO> GetAllUsers();

        [OperationContract]
        UserInfoDTO GetUserById(int id);

        [OperationContract]
        IList<ChatDTO> GetChatsOfUser(int id);

        [OperationContract]
        IList<UserInfoDTO> GetUserFriends(int id);

        [OperationContract]
        IList<UserInfoDTO> GetMutualFriends(int id1, int id2);

        [OperationContract]
        IList<UserInfoDTO> GetWayBetweenUsers(int id1, int id2);

        // PUT methods

        [OperationContract]
        OperationResultDTO UpdateUser(UserInfoDTO newUser);

        [OperationContract]
        OperationResultDTO DeleteUser(int id);

        // Account related things

        [OperationContract]
        OperationResultDTO AddAccount(AccountDTO model);

        [OperationContract]
        OperationResultDTO CheckCredentials(AccountDTO model);

        [OperationContract]
        Task<UserInfoDTO> FindUserByCredentials(AccountDTO model);

        [OperationContract]
        UserInfoDTO FindUserByUserName(string userName);
    }
}
