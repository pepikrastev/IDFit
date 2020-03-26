namespace IDFit.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
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

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(" ", error.Description);
                }

                return this.View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            this.ViewBag.roleId = roleId;

            var role = await this.roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                this.ViewBag.ErrorMessage = $"Role with id = {roleId} connot be found";
                return this.View("NotFound");
            }

            var viewModel = new List<UserRoleViewModel>();

            foreach (var user in this.userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if (await this.userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                viewModel.Add(userRoleViewModel);
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> viewModel, string roleId)
        {
            var role = await this.roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                this.ViewBag.ErrorMessage = $"Role with id = {roleId} connot be found";
                return this.View("NotFound");
            }

            for (int i = 0; i < viewModel.Count; i++)
            {
                var user = await this.userManager.FindByIdAsync(viewModel[i].UserId);

                IdentityResult result = null;

                if (viewModel[i].IsSelected && !(await this.userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await this.userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!viewModel[i].IsSelected && await this.userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await this.userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (viewModel.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return this.RedirectToAction("EditRole", new { Id = roleId });
                    }
                }
            }

            return this.RedirectToAction("EditRole", new { Id = roleId });
        }
    }
}
