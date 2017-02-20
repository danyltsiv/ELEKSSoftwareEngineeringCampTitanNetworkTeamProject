using BusinessLogicTier.DataAccesLayer.Entities;
using BusinessLogicTier.DataTranferObjects;
using BusinessLogicTier.EntityConverters;
using BusinessLogicTier.Providers.UserProviderInfrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using WCFService.DataTranferObjects;
using WCFService.EntityConverters;

namespace WCFService.Services
{
    public class UserService : IUserService
    {
        private AccountDataConverter _accountConverter;
        protected UserProvider UserProvider { get; private set; }
        private UserConverter _userConverter;
        private ChatConverter _chatConverter;

        public UserService()
        {
            _accountConverter = new AccountDataConverter();
            UserProvider = new UserProvider();
            _userConverter = new UserConverter();
            _chatConverter = new ChatConverter();
        }

        public ICollection<UserInfoDTO> GetAllUsers()
        {
            var users = UserProvider.GetAllUsers();
            return _userConverter.ToDataTransferEntityList(users);
        }

        public UserInfoDTO GetUserById(int id)
        {
            var user = UserProvider.GetUserById(id);
            return _userConverter.ToDataTransferEntity(user);
        }

        public bool UpdateUser(UserInfoDTO newUser)
        {
            var user = _userConverter.ToBusinessEntity(newUser);
            return UserProvider.UpdateUser(user);
        }

        public ICollection<ChatDTO> GetChatsOfUser(UserInfoDTO dto)
        {
            var user = _userConverter.ToBusinessEntity(dto);
            var list = UserProvider.GetChatsOfUser(user);
            return _chatConverter.ToDataTransferEntityList(list);
        }

        public ICollection<UserInfoDTO> GetUserFriends(UserInfoDTO dto)
        {
            var user = _userConverter.ToBusinessEntity(dto);
            var list = UserProvider.GetUserFriends(user);
            return _userConverter.ToDataTransferEntityList(list);
        }

        public IList<UserInfoDTO> GetMutualFriends(UserInfoDTO userInfo1, UserInfoDTO userInfo2)
        {
            var user1 = _userConverter.ToBusinessEntity(userInfo1);
            var user2 = _userConverter.ToBusinessEntity(userInfo2);
            var list = UserProvider.GetMutualFriends(user1, user2);
            return _userConverter.ToDataTransferEntityList(list);
        }

        public ICollection<UserInfoDTO> GetWayBetweenUsers(UserInfoDTO userInfo1, UserInfoDTO userInfo2)
        {
            var user1 = _userConverter.ToBusinessEntity(userInfo1);
            var user2 = _userConverter.ToBusinessEntity(userInfo2);
            var list = UserProvider.GetWayBetweenFriends(user1, user2);
            return _userConverter.ToDataTransferEntityList(list);
        }

        public async Task<bool> DeleteUser(UserInfoDTO dto)
        {
            var user = _userConverter.ToBusinessEntity(dto);
            var result = await UserProvider.DeleteUser(user);
            return result.Succeeded;
        }
        
        public async Task<bool> AddAccount(AccountDTO model)
        {
            var userModel = _accountConverter.ToBusinessEntity(model);
            var result = await UserProvider.RegisterUser(userModel);
            return result.Succeeded;
        }
        
        public async Task<bool> CheckCredentials(AccountDTO model)
        {
            User user = await UserProvider.FindUser(model.UserName, model.Password);
            return user != null;
        }

        public virtual void Dispose()
        {
            UserProvider.Dispose();
        }
    }
}
