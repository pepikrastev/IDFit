namespace IDFit.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using IDFit.Common;
    using IDFit.Data.Models;
    using IDFit.Services.Data;
    using IDFit.Services.Data.Diets;
    using IDFit.Web.ViewModels.Diets;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.CoachRoleName)]
    public class DietsController : Controller
    {
        private readonly IDietsService dietsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;

        public DietsController(IDietsService dietsService, UserManager<ApplicationUser> userManager, IUsersService usersService)
        {
            this.dietsService = dietsService;
            this.userManager = userManager;
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult CreateDiet()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateDiet(CreateDietViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                if ((viewModel.EndTime - viewModel.StartTime).Days > 1)
                {
                    var resutl = this.dietsService.CreateDiet(viewModel.Name, viewModel.StartTime, viewModel.EndTime);

                    if (resutl > -1)
                    {
                        return this.Redirect("/");
                    }
                }
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult AllDiets()
        {
            var viewModel = new AllDietsViewModel();
            var diets = this.dietsService.GetAllDiets<CreateDietViewModel>();
            viewModel.Diets = diets;

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteDiet(int id)
        {
            var result = this.dietsService.DeleteDiet(id);

            if (result <= -1)
            {
                return this.RedirectToAction("Error");
            }

            return this.RedirectToAction("AllDiets");
        }

        [HttpGet]
        public IActionResult EditDiet(int id)
        {
            var viewModel = this.dietsService.GetDietById<EditDietViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult EditDiet(EditDietViewModel model)
        {
            var diet = this.dietsService.GetDietById(model.Id);

            if (diet == null)
            {
                return this.RedirectToAction("Error");
            }
            else
            {
                var result = this.dietsService.AddDietInDb(diet, model.Name, model.StartTime, model.EndTime);
                if (result >= 0)
                {
                    return this.RedirectToAction("AllDiets");
                }

                return this.View(model);
            }
        }

        [HttpGet]
        public IActionResult EditUsersInDiet(int dietId)
        {
            this.ViewBag.dietId = dietId;

            var diet = this.dietsService.GetDietById(dietId);

            if (diet == null)
            {
                this.ViewBag.ErrorMessage = $"Diet with id = {dietId} connot be found";
                return this.View("NotFound");
            }

            var viewModel = new List<UserDietViewModel>();

            foreach (var user in this.userManager.Users)
            {
                var userDietViewModel = new UserDietViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if (user.DietId == diet.Id)
                {
                    userDietViewModel.IsSelected = true;
                }
                else
                {
                    userDietViewModel.IsSelected = false;
                }

                viewModel.Add(userDietViewModel);
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInDiet(List<UserDietViewModel> viewModel, int dietId)
        {
            var diet = this.dietsService.GetDietById(dietId);

            if (diet == null)
            {
                this.ViewBag.ErrorMessage = $"Role with id = {diet.Id} connot be found";
                return this.View("NotFound");
            }

            for (int i = 0; i < viewModel.Count; i++)
            {
                var user = this.usersService.GetUserById(viewModel[i].UserId);

                IdentityResult result = null;

                if (viewModel[i].IsSelected)
                {
                    user.DietId = dietId;
                }
                else if (!viewModel[i].IsSelected)
                {
                    user.DietId = null;
                }
                else
                {
                    continue;
                }

                // if (result.Succeeded)
                // {
                //    if (i < (viewModel.Count - 1))
                //    {
                //        continue;
                //    }
                //    else
                //    {
                //        return this.RedirectToAction("EditRole", new { Id = roleId });
                //    }
                // }
            }
            var resutl = this.dietsService.DbSaveChanges();

            return this.RedirectToAction("EditDiet", new { Id = dietId });
        }
    }
}
