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
       
        public DietsController(IDietsService dietsService)
        {
            this.dietsService = dietsService;
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
                        return this.RedirectToAction("AllDiets");
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
    }
}
