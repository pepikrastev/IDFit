namespace IDFit.Web.Controllers
{
    using System.Threading.Tasks;

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

            foreach (var training in user.Trainings)
            {
                model.Trainings.Add(training.Name);
            }

            foreach (var person in user.TrainedPeople)
            {
                model.Trainings.Add(person.UserName);
            }

            return this.View(model);
        }

        [HttpGet]
        public IActionResult EditUser(string id)
        {
            var model = this.usersService.GetUserById<EditUserViewModel>(id);

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
    }
}
