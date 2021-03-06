﻿namespace IDFit.Web.Controllers
{
    using System;
    using System.Collections.Generic;

    using IDFit.Common;
    using IDFit.Services.Data.Diets;
    using IDFit.Services.Data.Foods;
    using IDFit.Web.ViewModels.Diets;
    using IDFit.Web.ViewModels.Foods;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.CoachRoleName)]
    public class DietsController : Controller
    {
        private readonly IDietsService dietsService;
        private readonly IFoodsService foodsService;

        public DietsController(IDietsService dietsService, IFoodsService foodsService)
        {
            this.dietsService = dietsService;
            this.foodsService = foodsService;
        }

        [HttpGet]
        public IActionResult CreateDiet()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateDiet(CreateDietViewModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                if ((inputModel.EndTime - inputModel.StartTime).Days > 1 && (inputModel.EndTime - DateTime.UtcNow).Days > 1)
                {
                    var resutl = this.dietsService.CreateDiet(inputModel.Name, inputModel.StartTime, inputModel.EndTime, inputModel.Description);

                    if (resutl > -1)
                    {
                        return this.RedirectToAction("AllDiets");
                    }
                }
                else
                {
                    this.ModelState.AddModelError(" ", "Start Date must be earlier than End Date");
                }
            }

            return this.View(inputModel);
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
        public IActionResult EditDiet(EditDietViewModel viewModel)
        {
            var diet = this.dietsService.GetDietById(viewModel.Id);

            if (diet == null)
            {
                return this.RedirectToAction("Error");
            }
            else
            {
                if (this.ModelState.IsValid)
                {
                    if ((viewModel.EndTime - viewModel.StartTime).Days > 1 && (viewModel.EndTime - DateTime.UtcNow).Days > 1)
                    {
                        this.dietsService.EditDiet(diet, viewModel.Name, viewModel.StartTime, viewModel.EndTime, viewModel.Description);

                        return this.RedirectToAction("AllDiets");
                    }
                    else
                    {
                        this.ModelState.AddModelError(" ", "Start Date must be earlier than End Date");
                    }
                }
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult AddFoodsInDiet(int dietId)
        {
            this.ViewBag.dietId = dietId;

            var diet = this.dietsService.GetDietById(dietId);
            if (diet == null)
            {
                this.ViewBag.ErrorMessage = $"Diet with id = {dietId} connot be found";
                return this.View("NotFound");
            }

            this.ViewBag.dietName = diet.Name;

            var viewModel = new List<FoodViewModel>();
            var foods = this.foodsService.GetAllFoodsForDiet(diet.Id);

            foreach (var food in foods)
            {
                var foodViewModel = new FoodViewModel
                {
                    Id = food.Id,
                    Name = food.Name,
                    DietId = food.DietId,
                    Quantity = food.Quantity,
                    Weight = food.Weight,
                };

                if (food.DietId == null)
                {
                    foodViewModel.IsSelected = false;
                }
                else if (food.DietId == diet.Id)
                {
                    foodViewModel.IsSelected = true;
                }

                viewModel.Add(foodViewModel);
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult AddFoodsInDiet(List<FoodViewModel> viewModel, int dietId)
        {
            var diet = this.dietsService.GetDietById(dietId);
            if (diet == null)
            {
                this.ViewBag.ErrorMessage = $"Diet with id = {dietId} connot be found";
                return this.View("NotFound");
            }

            for (int i = 0; i < viewModel.Count; i++)
            {
                var food = this.foodsService.GetFoodById(viewModel[i].Id);

                if (viewModel[i].IsSelected && viewModel[i].DietId == null)
                {
                    diet.Foods.Add(food);
                }
                else
                {
                    diet.Foods.Remove(food);
                }
            }

            this.dietsService.EditDietInDb(diet);

            return this.RedirectToAction("EditDiet", new { Id = dietId });
        }
    }
}
