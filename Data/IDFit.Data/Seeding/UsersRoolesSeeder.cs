namespace IDFit.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using IDFit.Common;
    using IDFit.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersRoolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedUserAsync(dbContext, userManager);

            await UserToRoleAsync(roleManager, userManager, dbContext);
        }

        private async Task UserToRoleAsync(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            var user = await userManager.FindByNameAsync(GlobalConstants.CoachUserName);

            if (user != null)
            {
                return;
            }

            var role = await roleManager.FindByNameAsync(GlobalConstants.CoachRoleName);

            if (role != null)
            {
                return;
            }

            dbContext.UserRoles.Add(new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = user.Id,
            });

            await dbContext.SaveChangesAsync();
        }

        private async Task SeedUserAsync(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.FindByNameAsync(GlobalConstants.CoachUserName);

            if (user != null)
            {
                return;
            }

            await userManager.CreateAsync(
                    new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = GlobalConstants.CoachUserName,
                        FirstName = "Krum",
                        LastName = "Asparuh",
                        Age = 25,
                        Email = GlobalConstants.CoachUserName,
                        EmailConfirmed = true,
                        PhoneNumber = "1234567890",
                    },
                    "123456");
        }
    }
}
