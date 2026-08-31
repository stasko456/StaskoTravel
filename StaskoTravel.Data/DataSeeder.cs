using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StaskoTravel.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.DataAccess
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            List<string> roles = new List<string> { "Admin", "User" };

            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
        }

        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var adminExists = await userManager.FindByNameAsync("admin");

            if (adminExists != null)
            {
                return;
            }

            var admin = new User
            {
                FirstName = "Admin",
                LastName = "Adminov",
                UserName = "admin",
                Email = "admin@admin.com",
            };

            await userManager.CreateAsync(admin, "Admin1234*");

            if (!await userManager.IsInRoleAsync(admin, "Admin"))
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        public static async Task SeedTestUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var userExists = await userManager.FindByNameAsync("stasko456");

            if (userExists != null)
            {
                return;
            }

            var user = new User
            {
                FirstName = "Stanislav",
                LastName = "Dimov",
                UserName = "stasko456",
                Email = "stasioBG00@gmail.com",
            };

            await userManager.CreateAsync(user, "Taina1234*");

            if (!await userManager.IsInRoleAsync(user, "User"))
            {
                await userManager.AddToRoleAsync(user, "User");
            }
        }
    }
}