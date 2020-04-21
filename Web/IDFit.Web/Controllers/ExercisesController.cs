namespace IDFit.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IDFit.Common;
    using IDFit.Data;
    using IDFit.Data.Models;
    using IDFit.Services.Data.Exercises;
    using IDFit.Services.Data.Tools;
    using IDFit.Web.ViewModels.Exercises;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles = GlobalConstants.CoachRoleName)]
    public class ExercisesController : BaseController
    {
        private readonly IToolsService toolsService;
        private readonly IExercisesService exercisesService;
        private readonly ApplicationDbContext db;

        public ExercisesController(IToolsService toolsService, IExercisesService exercisesService, ApplicationDbContext db)
        {
            this.toolsService = toolsService;
            this.exercisesService = exercisesService;
            this.db = db;
        }

        [HttpGet]
        public IActionResult CreateExercise()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExercise(CreateExerciseViewModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                await this.exercisesService.CreateExercise(inputModel);
                return this.Redirect("/Exercises/AllExercises");
            }

            return this.View(inputModel);
        }

        [HttpGet]
        public IActionResult AllExercises()
        {
            var viewModel = new List<ExerciseViewModel>();
            // var exercises = this.exercisesService.GetAllExercise<ExerciseViewModel>().ToList();

            var exersiseModel = this.exercisesService.GetAllExerciseViewModel();
            viewModel = exersiseModel.ToList();
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteExercise(int id)
        {
            var result = this.exercisesService.DeleteExercise(id);

            if (result <= -1)
            {
                return this.RedirectToAction("Error");
            }

            return this.RedirectToAction("AllExercises");
        }

        [HttpGet]
        public IActionResult EditExercise(int id)
        {
            // var viewModel = this.exercisesService.GetExerciseById<EditExerciseViewModel>(id);
            var exercise = this.exercisesService.GetExerciseById(id);
            if (exercise == null)
            {
                return this.RedirectToAction("Error");
            }

            var toolsModel = this.exercisesService.GetAllToolsForExercise<ToolsListViewModel>(id);

            var viewModel = new EditExerciseViewModel
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Description = exercise.Description,
                Tools = toolsModel.ToList(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult EditExercise(EditExerciseViewModel viewModel)
        {
            // var exercise = this.exercisesService.GetExerciseById(viewModel.Id);
            var result = this.exercisesService.EditExercise(viewModel);
            if (result.Result > 0)
            {
                return this.Redirect("/Exercises/AllExercises");
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult AddToolsInExercise(int exerciseId)
        {
            this.ViewBag.exerciseId = exerciseId;

            var exercise = this.exercisesService.GetExerciseById(exerciseId);
            if (exercise == null)
            {
                this.ViewBag.ErrorMessage = $"Exercise with id = {exerciseId} connot be found";
                return this.View("NotFound");
            }
            this.ViewBag.exerciseName = exercise.Name;

            var viewModel = new List<ToolsListViewModel>();

            // var tools = this.exercisesService.GetAllToolsForExercise(exerciseId).ToList();
            var tools = this.toolsService.GetAllTools();
            foreach (var tool in tools)
            {
                var toolsListViewModel = new ToolsListViewModel
                {
                    Id = tool.Id,
                    Name = tool.Name,
                    Details = tool.Details,
                };

                if (this.db.ExercisesTools.Any(x => x.ExerciseId == exerciseId && x.ToolId == tool.Id))
                {
                    toolsListViewModel.IsSelected = true;
                }
                else
                {
                    toolsListViewModel.IsSelected = false;
                }

                viewModel.Add(toolsListViewModel);
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult AddToolsInExercise(List<ToolsListViewModel> viewModel, int exerciseId)
        {
            var exercise = this.exercisesService.GetExerciseById(exerciseId);
            if (exercise == null)
            {
                this.ViewBag.ErrorMessage = $"Exercise with id = {exerciseId} connot be found";
                return this.View("NotFound");
            }

            foreach (var item in this.db.ExercisesTools)
            {
                if (item.ExerciseId == exercise.Id)
                {
                    this.db.Entry(item).State = EntityState.Deleted;
                }
            }

            foreach (var tool in viewModel)
            {
                if (tool.IsSelected)
                {
                    this.db.ExercisesTools.Add(new ExerciseTool
                    {
                        ExerciseId = exerciseId,
                        ToolId = tool.Id,
                    });
                }
            }

            this.db.SaveChanges();
            return this.Redirect("/Exercises/AllExercises");
        }
    }
}
