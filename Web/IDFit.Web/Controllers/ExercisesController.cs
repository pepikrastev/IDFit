namespace IDFit.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IDFit.Common;
    using IDFit.Services.Data.Exercises;
    using IDFit.Services.Data.Tools;
    using IDFit.Web.ViewModels.Exercises;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.CoachRoleName)]
    public class ExercisesController : BaseController
    {
        private readonly IToolsService toolsService;
        private readonly IExercisesService exercisesService;

        public ExercisesController(IToolsService toolsService, IExercisesService exercisesService)
        {
            this.toolsService = toolsService;
            this.exercisesService = exercisesService;
        }

        [HttpGet]
        public IActionResult CreateExercise()
        {
            var viewModel = new CreateExerciseViewModel();
            var toolsViewModel = new List<ToolsListViewModel>();

            var tools = this.toolsService.GetAllTools().ToList();

            for (int i = 0; i < tools.Count(); i++)
            {
                var toolView = new ToolsListViewModel
                {
                    Id = tools[i].Id,
                    Name = tools[i].Name,
                    Details = tools[i].Details,
                };
                toolsViewModel.Add(toolView);
            }

            viewModel.Tools = toolsViewModel.ToList();
  
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExercise(CreateExerciseViewModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                await this.exercisesService.CreateExercise(inputModel);
                return this.Redirect("/");
            }
            return this.View(inputModel);
        }

        [HttpGet]
        public IActionResult AllExercises()
        {
            var viewModel = new List<ExerciseViewModel>();
            // var exercises = this.exercisesService.GetAllExercise<ExerciseViewModel>().ToList();

            var exersise = this.exercisesService.GetAllExercise();
            viewModel = exersise.ToList();
            return this.View(viewModel);
        }
    }
}
