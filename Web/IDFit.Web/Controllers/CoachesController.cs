namespace IDFit.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IDFit.Common;
    using IDFit.Data;
    using IDFit.Data.Common.Repositories;
    using IDFit.Data.Models;
    using IDFit.Services.Data;
    using IDFit.Services.Data.Diets;
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Coaches;
    using IDFit.Web.ViewModels.Diets;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CoachesController : Controller
    {
        private readonly ICoachesService coachesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;
        private readonly IDietsService dietsService;

        public CoachesController(ICoachesService coachesService, UserManager<ApplicationUser> userManager, IUsersService usersService, IDietsService dietsService)
        {
            this.coachesService = coachesService;
            this.userManager = userManager;
            this.usersService = usersService;
            this.dietsService = dietsService;
        }

        [HttpGet]
        public IActionResult All()
        {
            var viewModel = new AllCoachesViewModel();
            var coaches = this.coachesService.GetAllCoaches<CoachViewModel>();
            viewModel.Coaches = coaches;

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Coach(string id)
        {
            // take user.CoachId for buttons to users to pick or remove coach
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            this.ViewBag.userCoachId = user.CoachId;

            var viewModel = this.coachesService.GetCoachById<CoachViewModel>(id);
            if (viewModel == null)
            {
                return this.RedirectToAction("Error");
            }

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.CoachRoleName)]
        public IActionResult CoachUpdateHisUser(string userId)
        {
            var model = this.usersService.GetUserById<TrainedUserViewModel>(userId);

            if (model.CoachId == null)
            {
                return this.RedirectToAction("Error");
            }

            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.CoachRoleName)]
        public IActionResult AddDietToUser(string userId)
        {
            this.ViewBag.userId = userId;
            this.ViewBag.user = this.usersService.GetUserById(userId);
            var viewModel = new AllDietsViewModel();
            var diets = this.dietsService.GetAllDiets<CreateDietViewModel>();
            viewModel.Diets = diets;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.CoachRoleName)]
        public IActionResult AddDietToUser(int dietId, string userId)
        {
            var diet = this.dietsService.GetDietById(dietId);
            if (diet == null)
            {
                return this.RedirectToAction("Error");
            }

            var user = this.usersService.GetUserById(userId);
            if (diet == null)
            {
                return this.RedirectToAction("Error");
            }

            var result = this.coachesService.AddDiet(diet, user);

            if (result < 0)
            {
                return this.RedirectToAction("Error");
            }

            return this.RedirectToAction("CoachUpdateHisUser", new { userId = user.Id });
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.CoachRoleName)]
        public async Task<IActionResult> RemoveDietFromUser(int dietId, string userId)
        {
            var diet = this.dietsService.GetDietById(dietId);
            if (diet == null)
            {
                return this.RedirectToAction("Error");
            }

            var user = this.usersService.GetUserById(userId);
            if (diet == null)
            {
                return this.RedirectToAction("Error");
            }

            var result = await this.coachesService.RemoveDietAsync(diet, user);
            if (result < 0)
            {
                return this.RedirectToAction("Error");
            }

            return this.RedirectToAction("CoachUpdateHisUser", new { userId = user.Id });
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.CoachRoleName)]
        public IActionResult AddOrRemoveTrainigForUser(string userId)
        {
            this.ViewBag.userId = userId;
            var user = this.usersService.GetUserById(userId);

            if (userId == null)
            {
                this.ViewBag.ErrorMessage = $"Users with id = {userId} connot be found";
                return this.View("NotFound");
            }

            this.ViewBag.userUsername = user.UserName;

            var viewModel = this.coachesService.GetListOfTrainigsForUser(userId).ToList();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.CoachRoleName)]
        public async Task<IActionResult> AddOrRemoveTrainigForUser(List<TrainigForListViewModel> viewModel, string userId)
        {
            var user = this.usersService.GetUserById(userId);
            if (user == null)
            {
                this.ViewBag.ErrorMessage = $"User with id = {userId} connot be found";
                return this.View("NotFound");
            }

            var result = await this.coachesService.AddOrRemoveTrainingFromUserAsync(userId, viewModel);

            if (result > 0)
            {
                return this.RedirectToAction("CoachUpdateHisUser", new { userId = user.Id });
            }

            return this.View(viewModel);
        }
    }
}
