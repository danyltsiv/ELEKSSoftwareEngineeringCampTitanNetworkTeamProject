using BusinessLogicTier.DataTranferObjects;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using WCFService.DataTranferObjects;

namespace WCFService.Services
{
    [ServiceContract]
    public interface IUserService
    {
        // GET methods

        [OperationContract]
        ICollection<UserInfoDTO> GetAllUsers();

        [OperationContract]
        UserInfoDTO GetUserById(int id);

        [OperationContract]
        ICollection<ChatDTO> GetChatsOfUser(UserInfoDTO dto);

        [OperationContract]
        ICollection<UserInfoDTO> GetUserFriends(UserInfoDTO dto);

        [OperationContract]
        IList<UserInfoDTO> GetMutualFriends(UserInfoDTO userInfo1, UserInfoDTO userInfo2);

        [OperationContract]
        ICollection<UserInfoDTO> GetWayBetweenUsers(UserInfoDTO id1, UserInfoDTO id2);

        // PUT methods

        [OperationContract]
        bool UpdateUser(UserInfoDTO newUser);

        [OperationContract]
        Task<bool> DeleteUser(UserInfoDTO dto);

        // Account related things

        [OperationContract]
        Task<bool> AddAccount(AccountDTO model);

        [OperationContract]
        Task<bool> CheckCredentials(AccountDTO model);
    }
}
