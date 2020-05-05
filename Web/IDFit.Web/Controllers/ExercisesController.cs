namespace IDFit.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IDFit.Common;
    using IDFit.Services.Data.Exercises;
    using IDFit.Web.ViewModels.Exercises;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.CoachRoleName)]
    public class ExercisesController : BaseController
    {
        private readonly IExercisesService exercisesService;

        public ExercisesController(IExercisesService exercisesService)
        {
            this.exercisesService = exercisesService;
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

            var viewModel = this.exercisesService.GetToolsListForExercise(exerciseId).ToList();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToolsInExercise(List<ToolsListViewModel> viewModel, int exerciseId)
        {
            var exercise = this.exercisesService.GetExerciseById(exerciseId);
            if (exercise == null)
            {
                this.ViewBag.ErrorMessage = $"Exercise with id = {exerciseId} connot be found";
                return this.View("NotFound");
            }

            var result = await this.exercisesService.AddOrRemoveToolsForExerciseAsync(exerciseId, viewModel);

            if (result > 0)
            {
                return this.Redirect("/Exercises/AllExercises");
            }

            return this.View(viewModel);
        }
    }
}
