using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinkerJems.Web2
{
    public class SeedData
    {
        public const string AdminRoleName = "Admin";

        public const string SecurityPolicy_Add = "CanAdd";
        public const string SecurityPolicy_Edit = "CanEdit";
        public const string SecurityPolicy_Delete = "CanDelete";

        internal static void EnsureInitialized(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var role = roleManager.FindByNameAsync(AdminRoleName).Result;
            if (role == null)
            {
                role = new IdentityRole(AdminRoleName);
                roleManager.CreateAsync(role).GetAwaiter().GetResult();
            }

            var adminUser = "admin@tinkerjems.com";
            var user = userManager.FindByNameAsync(adminUser).Result;
            if (user != null)
            {
                userManager.AddToRoleAsync(user, AdminRoleName).GetAwaiter().GetResult();
            }
        }
    }
}
