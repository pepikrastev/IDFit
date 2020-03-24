namespace IDFit.Web.Areas.Administration.Controllers
{
    using System.Linq;

    using IDFit.Common;
    using IDFit.Data;
    using IDFit.Web.Controllers;
    using IDFit.Web.ViewModels.Administration.Administration;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    // [Authorize(Roles = GlobalConstants.CoachRoleName)]

    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly ApplicationDbContext db;

        public AdministrationController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult CreateCoach()
        {
            var viewModel = new AllUsersViewModel();
            var role = this.db.Roles.FirstOrDefault(x => x.Name == GlobalConstants.CoachRoleName);
            var users = this.db.Users
                .Where(x => x.Roles.Any() == false)
                .Select(x => new UsersInfoViewModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    ImageUrl = x.ImageUrl,
                });
            viewModel.Users = users;

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateCoach(string username)
        {
            var user = this.db.Users.FirstOrDefault(x => x.UserName == username);
            var role = this.db.Roles.FirstOrDefault(x => x.Name == GlobalConstants.CoachRoleName);

            this.db.UserRoles.Add(new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = user.Id,
            });

            this.db.SaveChanges();

            return this.Redirect("/");
        }
    }
}
