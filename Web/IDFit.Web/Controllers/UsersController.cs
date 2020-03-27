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
        public async Task<IActionResult> UserProfile(/*string name*/)
        {
            // if i give -- asp-route-name="@User.Identity.Name" to view I can use the name to find user
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
    }
}
