using BusinessLogicTier.DataAccesLayer.Entities;
using WCFService.DataTranferObjects;
using WCFService.EntityConverters;
using BusinessLogicTier.Providers.UserProviderInfrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BusinessLogicTier;
using WCFService.DataTransferObjects;

namespace WCFService.Services
{
    //Rh56io9
    /// <summary>
    /// Class UserService.
    /// </summary>
    /// <seealso cref="WCFService.Services.IUserService" />
    public class UserService : IUserService
    {
        private AccountDataConverter _accountConverter;
        protected UserProvider UserProvider { get; private set; }
        private UserConverter _userConverter;
        private ChatConverter _chatConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        public UserService()
        {
            _accountConverter = new AccountDataConverter();
            UserProvider = new UserProvider();
            _userConverter = new UserConverter();
            _chatConverter = new ChatConverter();
        }

        /// <summary>
        /// Return list of all users
        /// </summary>
        /// <returns>List of users</returns>
        public IList<UserInfoDTO> GetAllUsers()
        {
            Logger.log.Debug("at WCFService.UserService.GetAllUsers");
            var users = UserProvider.GetUserQueryable();
            return _userConverter.ToDataTransferEntityList(users.ToList());
        }

        /// <summary>
        /// Get concrete user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User info</returns>
        public UserInfoDTO GetUserById(int id)
        {
            Logger.log.Debug("at WCFService.UserService.GetUserById");
            var user = UserProvider.GetUserById(id);
            return _userConverter.ToDataTransferEntity(user);
        }

        /// <summary>
        /// Update specific user info
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>OperationResultDTO</returns>
        public OperationResultDTO UpdateUser(UserInfoDTO newUser)
        {
            Logger.log.Debug("at WCFService.UserService.UpdateUser");
            var user = _userConverter.ToBusinessEntity(newUser);
            var identity = UserProvider.UpdateUser(user);
            var result = new OperationResultDTO()
            {
                Result = identity.Result,
                Info = identity.Info
            };
            return result;
        }

        /// <summary>
        /// Get list of chats, this user has acces to them
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of chats</returns>
        public IList<ChatDTO> GetChatsOfUser(int id)
        {
            Logger.log.Debug("at WCFService.UserService.GetChatsOfUser");
            var list = UserProvider.GetChatsOfUser(new User() { Id = id });
            return _chatConverter.ToDataTransferEntityList(list.ToList());
        }

        /// <summary>
        /// Get all friends of current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of users</returns>
        public IList<UserInfoDTO> GetUserFriends(int id)
        {
            Logger.log.Debug("at WCFService.UserService.GetUserFriends");
            var list = UserProvider.GetUserFriends(new User() { Id = id });
            return _userConverter.ToDataTransferEntityList(list.ToList());
        }

        /// <summary>
        /// Get mutual friends between two users
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns>List of users</returns>
        public IList<UserInfoDTO> GetMutualFriends(int id1, int id2)
        {
            Logger.log.Debug("at WCFService.UserService.GetMutualFriends");
            var user1 = new User() { Id = id1 };
            var user2 = new User() { Id = id2 };
            var list = UserProvider.GetMutualFriends(user1, user2);
            return _userConverter.ToDataTransferEntityList(list.ToList());
        }

        /// <summary>
        /// Get way between two users
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns>List of users</returns>
        public IList<UserInfoDTO> GetWayBetweenUsers(int id1, int id2)
        {
            Logger.log.Debug("at WCFService.UserService.GetWayBetweenUsers");
            var user1 = new User() { Id = id1 };
            var user2 = new User() { Id = id2 };
            var list = UserProvider.GetWayBetweenFriends(user1, user2);
            return _userConverter.ToDataTransferEntityList(list.ToList());
        }

        /// <summary>
        /// Delete user forever
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OperationResultDTO</returns>
        public OperationResultDTO DeleteUser(int id)
        {
            Logger.log.Debug("at WCFService.UserService.DeleteUser");
            var identity = UserProvider.DeleteUser(new User() { Id = id });
            var result = new OperationResultDTO()
            {
                Result = identity.Result,
                Info = identity.Info
            };
            return result;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>OperationResultDTO</returns>
        public OperationResultDTO AddAccount(AccountDTO model)
        {
            Logger.log.Debug("at WCFService.UserService.AddAccount");
            var userModel = _accountConverter.ToBusinessEntity(model);
            var identity = UserProvider.RegisterUser(userModel);
            var result = new OperationResultDTO()
            {
                Result = identity.Result,
                Info = identity.Info
            };
            return result;
        }

        /// <summary>
        /// Check, if user exist in the db
        /// </summary>
        /// <param name="model"></param>
        /// <returns>OperationResultDTO</returns>
        public OperationResultDTO CheckCredentials(AccountDTO model)
        {
            Logger.log.Debug("at WCFService.UserService.CheckCredentials");
            var user = FindUserByCredentials(model);
            var result = new OperationResultDTO()
            {
                Result = user != null,
                Info = "User not found"
            };
            return result;
        }

        /// <summary>
        /// Get specific user by his credentials
        /// </summary>
        /// <param name="model"></param>
        /// <returns>User info</returns>
        public async Task<UserInfoDTO> FindUserByCredentials(AccountDTO model)
        {
            Logger.log.Debug("at WCFService.UserService.FindUser");
            var user = await UserProvider.FindUser(model.UserName, model.Password);
            return user == null ? null : _userConverter.ToDataTransferEntity(user);
        }

        /// <summary>
        /// Get user info by his login
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>User info</returns>
        public UserInfoDTO FindUserByUserName(string userName)
        {
            Logger.log.Debug("at WCFService.UserService.FindUserByUserName");
            var user = UserProvider.FindByLogin(userName);
            return user == null ? null : _userConverter.ToDataTransferEntity(user);
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public virtual void Dispose()
        {
            UserProvider.Dispose();
        }
    }
}
