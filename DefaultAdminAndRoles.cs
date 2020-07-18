using Microsoft.AspNetCore.Identity;
using SupermarketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApp
{
    public static class DefaultAdminAndRoles
    {
        public static void CreateDefaultAdminAndRoles(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            DefaultRoles(roleManager);
            DefaultAdmin(userManager);
        }

        private static void DefaultRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("SalesRep").Result)
            {
                var role = new IdentityRole
                {
                    Name = "SalesRep"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }
        private static void DefaultAdmin(UserManager<AppUser> userManager)
        {
            if (userManager.FindByNameAsync("superadmin").Result == null)
            {
                var user = new AppUser
                {
                    UserName = "superadmin",
                    Email = "superadmin@localhost.com"
                };
                var result = userManager.CreateAsync(user, "Elvis1$").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator");
                }
            }
        }
    }
}
