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

            var tools = this.toolsService.GetAllTools();
            foreach (var toolModel in toolsViewModel)
            {
                if (toolModel.IsSelected)
                {
                }
            }

            viewModel.Tools = toolsViewModel;
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
            return this.View();
        }
    }
}
