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

            AddUserToAdminRole(userManager, "admin@tinkerjems.com");
            AddUserToAdminRole(userManager, "zachary.reiss123@gmail.com");
        }

        private static void AddUserToAdminRole(UserManager<IdentityUser> userManager, string userEmail)
        {
            var user = userManager.FindByNameAsync(userEmail).Result;
            if (user != null)
            {
                userManager.AddToRoleAsync(user, AdminRoleName).GetAwaiter().GetResult();
            }
        }
    }
}
