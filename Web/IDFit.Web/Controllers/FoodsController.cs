namespace IDFit.Web.Controllers
{
    using IDFit.Common;
    using IDFit.Data.Models;
    using IDFit.Services.Data.Foods;
    using IDFit.Web.ViewModels.Foods;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.CoachRoleName)]
    public class FoodsController : Controller
    {
        private readonly IFoodsService foodsService;

        public FoodsController(IFoodsService foodsService)
        {
            this.foodsService = foodsService;
        }

        [HttpGet]
        public IActionResult AllFoods()
        {
            var viewModel = new AllFoodsViewModel();
            var foods = this.foodsService.GetAllFoods<FoodViewModel>();
            viewModel.Foods = foods;

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult EditFood(int id)
        {
            var viewModel = this.foodsService.GetFoodById<FoodViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult EditFood(FoodViewModel viewModel)
        {
            var result = this.foodsService.EditFood(viewModel.Id, viewModel.Name, viewModel.Quantity, viewModel.Weight);

            if (result <= -1)
            {
                return this.View(viewModel);
            }
            else
            {
                return this.RedirectToAction("AllFoods");
            }
        }
    }
}
