namespace IDFit.Web.Controllers
{
    using IDFit.Common;
    using IDFit.Services.Data.Diets;
    using IDFit.Web.ViewModels.Diets;
    using Microsoft.AspNetCore.Authorization;
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
                var resutl = this.dietsService.CreateDiet(viewModel.Name, viewModel.StartTime, viewModel.EndTime);

             //   int result = DateTime.Compare(date1, date2);
                if (resutl > -1)
                {
                    return this.Redirect("/");
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
    }
}
