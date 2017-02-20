using BusinessLogicTier.Authentication;
using BusinessLogicTier.DataAccesLayer.Entities;
using BusinessLogicTier.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogicTier.Authentication.IdentityModel;

namespace BusinessLogicTier.Providers.UserProviderInfrastructure
{
    public abstract class AccountRepository : UserExtension
    {
        protected ApplicationUserManager UserManager { get; private set; }

        public AccountRepository() 
            : base()
        {
            UserManager = new ApplicationUserManager(new CustomUserStore(Context));
        }

        public async Task<IdentityResult> RegisterUser(AccountData userModel)
        {
            IdentityResult result = null;
            User user = new User
            {
                UserName = userModel.UserName
            };
            
            try
            {
                result = await UserManager.CreateAsync(user, userModel.Password);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return result;
        }

        public async Task<User> FindUser(string userName, string password)
        {
            User user = await UserManager.FindAsync(userName, password);
            return user;
        }

        public async Task<IdentityResult> DeleteUser(User user)
        {
            var result = await UserManager.DeleteAsync(user);
            return result;
        }

        public override void Dispose()
        {
            UserManager.Dispose();
            base.Dispose();
        }
    }
}
