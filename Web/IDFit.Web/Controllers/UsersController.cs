namespace IDFit.Web.Controllers
{
    using System.Threading.Tasks;
    using IDFit.Common;
    using IDFit.Data.Models;
    using IDFit.Services.Data;
    using IDFit.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;

        public UsersController(UserManager<ApplicationUser> userManager, IUsersService usersService)
        {
            this.userManager = userManager;
            this.usersService = usersService;
        }

        // [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.CoachRoleName}")]
        [HttpGet]
        [Authorize(Roles = "Administrator, Coach")]
        public IActionResult AllUsers()
        {
            var model = new AllUsersViewModel();
            var users = this.usersService.GetAllUsers<EditUserViewModel>();
            model.Users = users;

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {
            // var user = await this.userManager.FindByNameAsync(name);
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);

            if (user == null)
            {
                return this.RedirectToAction("Error");
            }

            // var viewModel = this.usersService.GetUser<UserProfilViewModel>(user.UserName);
            // return this.View(viewModel);
            var model = new UserProfilViewModel
            {
                Id = user.Id,
                ImageUrl = user.ImageUrl,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Description = user.Description,
                CoachId = user.CoachId,
                DietId = user.DietId,
            };

            var coach = await this.userManager.FindByIdAsync(user.CoachId);
            if (user.CoachId != null)
            {
                model.CoachUserName = coach.UserName;
            }

            if (await this.userManager.IsInRoleAsync(user, GlobalConstants.CoachRoleName))
            {
                var allUsersWithCoach = this.usersService.GetAllUsersWithCoach<UserWithCoachViewModel>();

                foreach (var person in allUsersWithCoach)
                {
                    model.TrainedPeople.Add(new UserWithCoachViewModel
                    {
                        Id = person.Id,
                        UserName = person.UserName,
                    });
                }
            }
            else
            {
                foreach (var training in user.Trainings)
                {
                    model.Trainings.Add(training.Name);
                }
            }

            return this.View(model);
        }

        [HttpGet]
        public IActionResult EditUser(string id)
        {
            var model = this.usersService.GetUserById<EditUserViewModel>(id);
            var userId = this.userManager.GetUserId(this.HttpContext.User);

            if (model.Id != userId)
            {
                return this.RedirectToAction("Error");
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await this.userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                return this.RedirectToAction("Error");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Age = model.Age;
            user.Description = model.Description;
            user.ImageUrl = model.ImageUrl;
            user.PhoneNumber = model.PhoneNumber;

            var result = await this.userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return this.RedirectToAction($"UserProfile");
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(" ", error.Description);
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCoachToUser(string coachId)
        {
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);

            var result = this.usersService.AddCoach(coachId, user);

            if (result > -1)
            {
                return this.RedirectToAction("UserProfile");
            }

            return this.RedirectToAction("Error");
        }
    }
}
