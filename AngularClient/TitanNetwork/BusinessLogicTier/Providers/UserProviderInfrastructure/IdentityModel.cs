using BusinessLogicTier.DataAccesLayer.Context;
using BusinessLogicTier.DataAccesLayer.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTier.Authentication
{
    public class IdentityModel
    {
        public class CustomUserRole : IdentityUserRole<int> { }
        public class CustomUserClaim : IdentityUserClaim<int> { }
        public class CustomUserLogin : IdentityUserLogin<int> { }

        public class CustomRole : IdentityRole<int, CustomUserRole>
        {
            public CustomRole() { }
            public CustomRole(string name) { Name = name; } 
        }

        public class CustomUserStore : UserStore<User, CustomRole, int,
            CustomUserLogin, CustomUserRole, CustomUserClaim>
        {
            public CustomUserStore(TitanNetworkContext context)
                : base(context)
            {
            }
        }

        public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
        {
            public CustomRoleStore(TitanNetworkContext context)
                : base(context)
            {
            }
        }
    }
}
