using System.Linq;
using crpt.Models;
using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace crpt.Data
{
    public static class DbInitializer
    {
        public async static Task Initialize(CryptoContext context, IServiceProvider services, string pw)
        {
            context.Database.EnsureCreated();

            if (context.Writers.Any())
            {
                return;
            }

            var roleAdmin = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
            await EnsureRoleAsync(roleAdmin, "Administrator");

            var userAdmin = services.GetRequiredService<UserManager<Writer>>();
            await EnsureAdminAsync(userAdmin, pw); 
        }

        private static async Task EnsureRoleAsync(RoleManager<IdentityRole<int>> roleManager, string role)
        {
            var alreadyExists = await roleManager.RoleExistsAsync(role);
            if(alreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole<int>(role));
        }

        private static async Task EnsureAdminAsync(UserManager<Writer> userManager, string userPw)
        {
            var testAdmin = await userManager.Users
                .Where(x => x.UserName == "admin@allinfo.hr")
                .SingleOrDefaultAsync();

            if(testAdmin!=null) return;

            testAdmin = new Writer
            {
                UserName = "admin@allinfo.hr",
                Email = "admin@allinfo.hr",
                isAdmin = true
            };
            
            await userManager.CreateAsync(testAdmin, userPw);
            await userManager.AddToRoleAsync(testAdmin, "Administrator");
        }
    }
}