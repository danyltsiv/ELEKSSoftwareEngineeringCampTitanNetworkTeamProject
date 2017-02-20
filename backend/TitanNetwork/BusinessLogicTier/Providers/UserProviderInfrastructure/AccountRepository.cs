using BusinessLogicTier.Authentication;
using BusinessLogicTier.DataAccesLayer.Entities;
using BusinessLogicTier.Models;
using BusinessLogicTier.Providers.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using static BusinessLogicTier.Authentication.IdentityModel;

namespace BusinessLogicTier.Providers.UserProviderInfrastructure
{
    /// <summary>
    /// Account specific functionality
    /// </summary>
    public abstract class AccountRepository : UserExtension
    {
        protected ApplicationUserManager UserManager { get; private set; }

        public AccountRepository() 
            : base()
        {
            UserManager = new ApplicationUserManager(new CustomUserStore(Context));
        }

        /// <summary>
        /// Add new user to the db
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>OperationResult</returns>
        public OperationResult RegisterUser(AccountData userModel)
        {
            Logger.log.Debug("at BusinessLogicTier.AccountRepository.RegisterUser");
            
            User user = new User
            {
                UserName = userModel.UserName
            };

            int id = 0;
            try
            {
                UserManager.Create(user, userModel.Password);
                Context.SaveChanges();
                id = Context.Users.ToList().Last().Id;
            }
            catch (Exception e)
            {
                Logger.log.Error(e.ToString());
                return new OperationResult()
                {
                    Result = false,
                    Info = e.ToString()
                };
            }
            return new OperationResult()
            {
                Result = true,
                Info = id
            };
        }

        /// <summary>
        /// Find user by account name and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>Found user</returns>
        public async Task<User> FindUser(string userName, string password)
        {
            Logger.log.Debug("at BusinessLogicTier.AccountRepository.FindUser");
            var user = await UserManager.FindAsync(userName, password);
            return user;
        }

        /// <summary>
        /// Delete user forever
        /// </summary>
        /// <param name="user"></param>
        /// <returns>OperationResult</returns>
        public OperationResult DeleteUser(User user)
        {
            Logger.log.Debug("at BusinessLogicTier.AccountRepository.DeleteUser");

            try
            {
                //UserManager.Delete(user);
                Context.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.log.Error("at BusinessLogicTier.AccountRepository.DeleteUser - " + e.ToString());
                return new OperationResult()
                {
                    Result = false,
                    Info = e.ToString()
                };
            }

            return new OperationResult()
            {
                Result = true,
                Info = ""
            };
        }

        /// <summary>
        /// Find user by login
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Found user</returns>
        public User FindByLogin(string login)
        {
            Logger.log.Debug("at BusinessLogicTier.AccountRepository.FindByLogin");
            return Context.Users.ToList().FirstOrDefault(x => x.UserName == login);
        }

        public override void Dispose()
        {
            UserManager.Dispose();
            base.Dispose();
        }
    }
}
