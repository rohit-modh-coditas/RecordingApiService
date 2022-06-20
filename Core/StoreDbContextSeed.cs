using Core.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    //public static class StoreDbContextSeed
    //{
    //    public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    //    {
    //        var administratorRole = new IdentityRole("Administrator");

    //        if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
    //        {
    //            await roleManager.CreateAsync(administratorRole);
    //        }

    //        var defaultUser = new ApplicationUser { UserName = "Rohit", Email = "test@g.com" };

    //        if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
    //        {
    //            await userManager.CreateAsync(defaultUser, "abc");
    //            await userManager.AddToRolesAsync(defaultUser, new[] { administratorRole.Name });
    //        }
    //    }
    //}
}
