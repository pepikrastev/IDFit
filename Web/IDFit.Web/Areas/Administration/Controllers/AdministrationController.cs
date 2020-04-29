namespace IDFit.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IDFit.Common;
    using IDFit.Data;
    using IDFit.Data.Models;
    using IDFit.Web.Controllers;
    using IDFit.Web.ViewModels.Administration.Administration;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext db;

        // RoleManager<IdentityUser> roleManager - is not working !!
        public AdministrationController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.db = db;
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

            var users = this.db.Users.Where(x => x.Roles.Any(x => x.RoleId == role.Id));

            foreach (var item in users)
            {
                model.Users.Add(item.UserName);
            }

            // is not working in azure
            // foreach (var user in this.userManager.Users)
            // {
            //    if (await this.userManager.IsInRoleAsync(user, role.Name))
            //    {
            //        model.Users.Add(user.UserName);
            //    }
            // }
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

            var users = this.db.Users.Where(x => x.Roles.Any(x => x.RoleId == role.Id)).ToList();

            foreach (var user in users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = true,
                };
                viewModel.Add(userRoleViewModel);
            }

            var usersUnselected = this.db.Users.Where(x => x.Roles.Any(x => x.RoleId != role.Id) || !x.Roles.Any()).ToList();

            foreach (var user in usersUnselected)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = false,
                };
                viewModel.Add(userRoleViewModel);
            }

            // is not working in azure
            // foreach (var user in this.userManager.Users)
            // {
            //    var userRoleViewModel = new UserRoleViewModel
            //    {
            //        UserId = user.Id,
            //        UserName = user.UserName,
            //    };

            // if (await this.userManager.IsInRoleAsync(user, role.Name))
            //    {
            //        userRoleViewModel.IsSelected = true;
            //    }
            //    else
            //    {
            //        userRoleViewModel.IsSelected = false;
            //    }

            // viewModel.Add(userRoleViewModel);
            // }
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

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await this.roleManager.FindByIdAsync(id);

            if (role == null)
            {
                this.ViewBag.ErrorMassage = $"Role with {id} cannot be found";
                return this.View("NotFound");
            }

            var result = await this.roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return this.RedirectToAction("AllRoles");
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(" ", error.Description);
            }

            return this.View("AllRoles");
        }
    }
}
