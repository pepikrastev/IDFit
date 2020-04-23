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

            await this.SeedUserAsync(userManager);

            await this.UserToRoleAsync(roleManager, userManager, dbContext);
        }

        private async Task UserToRoleAsync(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            var user = await userManager.FindByNameAsync(GlobalConstants.AdministratorUserName);

            // only first time have to be - if(user == null)
            if (user != null)
            {
                return;
            }

            var role = await roleManager.FindByNameAsync(GlobalConstants.AdministratorRoleName);

            // only first time have to be - if(role == null)
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

        private async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.FindByNameAsync(GlobalConstants.AdministratorUserName);

            if (user != null)
            {
                return;
            }

            await userManager.CreateAsync(
                    new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = GlobalConstants.AdministratorUserName,
                        FirstName = "Pepi",
                        LastName = "Hristov",
                        Age = 25,
                        Email = GlobalConstants.AdministratorUserName,
                        EmailConfirmed = true,
                        PhoneNumber = "1234567890",
                    },
                    "123456");
        }
    }
}
