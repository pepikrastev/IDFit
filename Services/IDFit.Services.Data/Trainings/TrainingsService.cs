namespace IDFit.Services.Data.Trainings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using IDFit.Services.Mapping;
    using IDFit.Data;
    using IDFit.Data.Common.Repositories;
    using IDFit.Data.Models;
    using IDFit.Web.ViewModels.Trainings;
    using Microsoft.EntityFrameworkCore;

    public class TrainingsService : ITrainingsService
    {
        private readonly IDeletableEntityRepository<Training> trainingRepository;
        private readonly ApplicationDbContext db;

        public TrainingsService(IDeletableEntityRepository<Training> trainingRepository, ApplicationDbContext db)
        {
            this.trainingRepository = trainingRepository;
            this.db = db;
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

            this.db.Trainings.Remove(training);
            return this.db.SaveChanges();
        }

        public IEnumerable<T> GetAllExerciseForTraining<T>(int trainingId)
        {
            // with custom mapping for one entity
            var exercises = this.db.Exercises.Where(x => x.TrainingsExercises.Any(x => x.TrainingId == trainingId));

            return exercises.To<T>().ToList();
        }

        public IEnumerable<TrainingViewModel> GetAllTrainings()
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

        public Training GetTrainingById(int id)
        {
            return this.trainingRepository.All()
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
