using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.Web
{
    public static class IdentityInitializer
    {
        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin"); // database tarafına bakıyor admin adında bir rol var mı yok mu
            if(adminRole == null)
            {
                await roleManager.CreateAsync(new AppRole { Name = "Admin" });
            }

            var memberRole = await roleManager.FindByNameAsync("Member");
            if(memberRole == null)
            {
                await roleManager.CreateAsync(new AppRole { Name = "Member" });
            }

            var adminUser = await userManager.FindByNameAsync("b171210311@sakarya.edu.tr");
            if(adminUser == null)
            {
                AppUser user = new AppUser()
                {
                    Name = "Anıl",
                    Surname = "Ateş",
                    UserName = "b171210311@sakarya.edu.tr",
                    Email = "anil@gmail.com"
                };
                await userManager.CreateAsync(user,"123");
                await userManager.AddToRoleAsync(user, "Admin"); // ilgili admin rolünü user a vermiş olduk
            }
        }
    }
}
