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
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Coaches;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CoachesController : Controller
    {
        private readonly ICoachesService coachesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoachesController(ICoachesService coachesService, UserManager<ApplicationUser> userManager)
        {
            this.coachesService = coachesService;
            this.userManager = userManager;
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
        public async Task<IActionResult> CoachUpdateHisUser(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            var usersCoach = await this.userManager.FindByIdAsync(user.CoachId);

            return this.View();
        }
    }
}
