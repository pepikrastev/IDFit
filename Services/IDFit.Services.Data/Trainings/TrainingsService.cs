namespace IDFit.Services.Data.Trainings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using IDFit.Data;
    using IDFit.Data.Common.Repositories;
    using IDFit.Data.Models;
    using IDFit.Services.Data.Exercises;
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Trainings;
    using Microsoft.EntityFrameworkCore;

    public class TrainingsService : ITrainingsService
    {
        private readonly IDeletableEntityRepository<Training> trainingRepository;
        private readonly ApplicationDbContext db;
        private readonly IExercisesService exercisesService;

        public TrainingsService(IDeletableEntityRepository<Training> trainingRepository, ApplicationDbContext db, IExercisesService exercisesService)
        {
            this.trainingRepository = trainingRepository;
            this.db = db;
            this.exercisesService = exercisesService;
        }

        public async Task<int> AddOrRemoveExerciseForTrainingAsync(int trainingId, List<ExerciseForListViewModel> viewModels)
        {
            foreach (var item in this.db.TrainingsExercises)
            {
                if (item.TrainingId == trainingId)
                {
                    this.db.Entry(item).State = EntityState.Deleted;
                }
            }

            foreach (var exercise in viewModels)
            {
                if (exercise.IsSelected)
                {
                    this.db.TrainingsExercises.Add(new TrainingExercise
                    {
                        TrainingId = trainingId,
                        ExerciseId = exercise.Id,
                    });
                }
            }

            return await this.db.SaveChangesAsync();
        }

        public async Task CreateTraining(CreateTrainingViewModel inputModel)
        {
            var training = new Training
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                TrainingTime = inputModel.TrainingTime,
            };
            await this.db.Trainings.AddAsync(training);
            await this.db.SaveChangesAsync();
        }

        public int DeleteTraining(int id)
        {
            var training = this.trainingRepository.All()
                .FirstOrDefault(x => x.Id == id);

            foreach (var item in this.db.TrainingsExercises)
            {
                if (item.TrainingId == id)
                {
                    this.db.Entry(item).State = EntityState.Deleted;
                }
            }

            foreach (var item in this.db.UsersTrainings)
            {
                if (item.TrainingId == id)
                {
                    this.db.Entry(item).State = EntityState.Deleted;
                }
            }

            this.db.Trainings.Remove(training);
            return this.db.SaveChanges();
        }

        public async Task<int> EditTraining(EditTrainingViewModel viewModel)
        {
            var training = this.trainingRepository.All()
                .Where(x => x.Id == viewModel.Id)
                .FirstOrDefault();

            training.Name = viewModel.Name;
            training.Description = viewModel.Description;
            training.TrainingTime = viewModel.TrainingTime;

            this.trainingRepository.Update(training);
            return await this.db.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllExerciseForTraining<T>(int trainingId)
        {
            var exercises = this.db.Exercises.Where(x => x.TrainingsExercises.Any(x => x.TrainingId == trainingId));

            // with custom mapping for one entity
            return exercises.To<T>().ToList();
        }

        public IEnumerable<Training> GetAllTrainings()
        {
            return this.trainingRepository.All().ToList();
        }

        public IEnumerable<TrainingViewModel> GetAllTrainingsViewModel()
        {
            var trainings = this.trainingRepository.All()
                .OrderBy(x => x.Name)
                .ToList();

            var trainingsListModel = new List<TrainingViewModel>();

            foreach (var training in trainings)
            {
                var exercisesCount = this.db.Exercises.Where(x => x.TrainingsExercises.Any(x => x.TrainingId == training.Id)).Count();

                var usersCount = this.db.Users.Where(x => x.UsersTrainings.Any(x => x.TrainingId == training.Id)).Count();

                var trainingModel = new TrainingViewModel
                {
                    Id = training.Id,
                    Name = training.Name,
                    Description = training.Description,
                    TrainingTime = training.TrainingTime,
                    ExercisesCount = exercisesCount,
                    UsersCount = usersCount,
                };

                trainingsListModel.Add(trainingModel);
            }

            return trainingsListModel;
        }

        public IEnumerable<ExerciseForListViewModel> GetExrciseListForTraining(int trainingId)
        {
            var exercises = this.exercisesService.GetAllExercise();
            var viewModel = new List<ExerciseForListViewModel>();

            foreach (var exercise in exercises)
            {
                var toolsCount = this.db.Tools.Where(x => x.ExercisesTools.Any(x => x.ExerciseId == exercise.Id)).Count();

                var exerciseModel = new ExerciseForListViewModel
                {
                    Id = exercise.Id,
                    Name = exercise.Name,
                    Description = exercise.Description,
                    // ToolsCount = toolsCount,
                };

                if (this.db.TrainingsExercises.Any(x => x.TrainingId == trainingId && x.ExerciseId == exercise.Id))
                {
                    exerciseModel.IsSelected = true;
                }
                else
                {
                    exerciseModel.IsSelected = false;
                }

                viewModel.Add(exerciseModel);
            }

            return viewModel.ToList();
        }

        public Training GetTrainingById(int id)
        {
            return this.trainingRepository.All()
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
