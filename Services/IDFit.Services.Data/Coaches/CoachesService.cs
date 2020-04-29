namespace IDFit.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IDFit.Common;
    using IDFit.Data;
    using IDFit.Data.Common.Repositories;
    using IDFit.Data.Models;
    using IDFit.Services.Data.Trainings;
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Coaches;
    using Microsoft.EntityFrameworkCore;

    public class CoachesService : ICoachesService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly ApplicationDbContext db;
        private readonly ITrainingsService trainingsService;

        public CoachesService(IDeletableEntityRepository<ApplicationUser> userRepository, ApplicationDbContext db, ITrainingsService trainingsService)
        {
            this.userRepository = userRepository;
            this.db = db;
            this.trainingsService = trainingsService;
        }

        public T GetCoachByName<T>(string name)
        {
            var user = this.userRepository.All()
                 .Where(x => x.FirstName == name)
                 .To<T>()
                 .FirstOrDefault();

            return user;
        }

        public T GetCoachById<T>(string id)
        {
            var user = this.userRepository.All()
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefault();

            return user;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<ApplicationUser> query = this.userRepository
                .All()
                .OrderBy(x => x.FirstName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllCoaches<T>()
        {
            var role = this.db.Roles.First(x => x.Name == GlobalConstants.CoachRoleName);

            IQueryable<ApplicationUser> query = this.userRepository.All()
                .Where(x => x.Roles.Any(r => r.RoleId == role.Id))
                .OrderBy(x => x.FirstName);

            return query.To<T>().ToList();
        }

        public int AddDiet(Diet diet, ApplicationUser user)
        {
            user.Diet = diet;
            user.DietId = diet.Id;
            this.userRepository.Update(user);

            diet.Users.Add(user);
            this.db.Diets.Update(diet);

            return this.db.SaveChanges();
        }

        public async Task<int> RemoveDietAsync(Diet diet, ApplicationUser user)
        {
            user.DietId = null;
            this.userRepository.Update(user);

            diet.Users.Remove(user);
            this.db.Diets.Update(diet);

            return await this.db.SaveChangesAsync();
        }

        public IEnumerable<TrainigForListViewModel> GetListOfTrainigsForUser(string userId)
        {
            var trainings = this.trainingsService.GetAllTrainings();
            var viewModel = new List<TrainigForListViewModel>();

            foreach (var trainig in trainings)
            {
                var exerciseCount = this.db.Exercises.Where(x => x.TrainingsExercises.Any(x => x.TrainingId == trainig.Id)).Count();

                var exerciseModel = new TrainigForListViewModel
                {
                    Id = trainig.Id,
                    Name = trainig.Name,
                    Description = trainig.Description,
                    TrainingTime = trainig.TrainingTime,
                    ExerciseCount = exerciseCount,
                };

                if (this.db.UsersTrainings.Any(x => x.UserId == userId && x.TrainingId == trainig.Id))
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

        public async Task<int> AddOrRemoveTrainingFromUserAsync(string userId, List<TrainigForListViewModel> viewModels)
        {
            foreach (var item in this.db.UsersTrainings)
            {
                if (item.UserId == userId)
                {
                    this.db.Entry(item).State = EntityState.Deleted;
                }
            }

            foreach (var training in viewModels)
            {
                if (training.IsSelected)
                {
                    this.db.UsersTrainings.Add(new UserTraining
                    {
                        UserId = userId,
                        TrainingId = training.Id,
                    });
                }
            }

            return await this.db.SaveChangesAsync();
        }
    }
}
