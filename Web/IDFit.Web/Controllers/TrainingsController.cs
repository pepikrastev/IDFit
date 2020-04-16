namespace IDFit.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IDFit.Common;
    using IDFit.Services.Data.Trainings;
    using IDFit.Web.ViewModels.Trainings;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.CoachRoleName)]
    public class TrainingsController : BaseController
    {
        private readonly ITrainingsService trainingsService;

        public TrainingsController(ITrainingsService trainingsService)
        {
            this.trainingsService = trainingsService;
        }

        [HttpGet]
        public IActionResult CreateTraining()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTraining(CreateTrainingViewModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                await this.trainingsService.CreateTraining(inputModel);
                return this.Redirect("/Trainings/AllTrainings");
            }

            return this.View(inputModel);
        }

        [HttpGet]
        public IActionResult AllTrainings()
        {
            var viewModel = new List<TrainingViewModel>();
            var trainings = this.trainingsService.GetAllTrainings();
            viewModel = trainings.ToList();
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteTraining(int id)
        {
            var result = this.trainingsService.DeleteTraining(id);

            if (result <= -1)
            {
                return this.RedirectToAction("Error");
            }

            return this.RedirectToAction("AllTrainings");
        }

        [HttpGet]
        public IActionResult EditTraining(int id)
        {
            var training = this.trainingsService.GetTrainingById(id);
            if (training == null)
            {
                return this.RedirectToAction("Error");
            }

            var exerciseListViewModel = this.trainingsService.GetAllExerciseForTraining<ExercisesListViewModel>(id).ToList();

            var trainingViewModel = new EditTrainingViewModel
            {
                Id = training.Id,
                Name = training.Name,
                Description = training.Description,
                TrainingTime = training.TrainingTime,
                Exercises = exerciseListViewModel,
            };
            return this.View(trainingViewModel);
        }
    }
}
