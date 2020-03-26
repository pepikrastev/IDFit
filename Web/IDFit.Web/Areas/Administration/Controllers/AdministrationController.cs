namespace IDFit.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using IDFit.Common;
    using IDFit.Data.Models;
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
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        // RoleManager<IdentityUser> roleManager - is not working !!
        public AdministrationController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                ApplicationRole applicationRole = new ApplicationRole
                {
                    Name = viewModel.RoleName,
                };

                IdentityResult result = await this.roleManager.CreateAsync(applicationRole);

                if (result.Succeeded)
                {
                    return this.RedirectToAction("AllRoles", "Administration");
                }
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult AllRoles()
        {
            var roles = this.roleManager.Roles;

            return this.View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await this.roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return this.RedirectToAction("Error");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
            };

            foreach (var user in this.userManager.Users)
            {
                if (await this.userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await this.roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                return this.RedirectToAction("Error");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await this.roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return this.RedirectToAction("AllRoles");
                }

                return this.View(model);
            }
        }
    }
}
