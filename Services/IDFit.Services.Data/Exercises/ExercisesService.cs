namespace IDFit.Services.Data.Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using IDFit.Data;
    using IDFit.Data.Common.Repositories;
    using IDFit.Data.Models;
    using IDFit.Services.Data.Tools;
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Exercises;
    using Microsoft.EntityFrameworkCore;

    public class ExercisesService : IExercisesService
    {
        private readonly IDeletableEntityRepository<Exercise> exercisesRepository;
        private readonly ApplicationDbContext db;
        private readonly IToolsService toolsService;

        public ExercisesService(IDeletableEntityRepository<Exercise> exercisesRepository, ApplicationDbContext db, IToolsService toolsService)
        {
            this.exercisesRepository = exercisesRepository;
            this.db = db;
            this.toolsService = toolsService;
        }

        public async Task CreateExercise(CreateExerciseViewModel inputModel)
        {
            var exercise = new Exercise
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
            };
            await this.db.Exercises.AddAsync(exercise);
            await this.db.SaveChangesAsync();
        }

        public int DeleteExercise(int id)
        {
            var exercise = this.exercisesRepository.All()
                .FirstOrDefault(x => x.Id == id);

            foreach (var item in this.db.ExercisesTools)
            {
                if (item.ExerciseId == id)
                {
                    this.db.Entry(item).State = EntityState.Deleted;
                }
            }

            this.db.Exercises.Remove(exercise);

           // set property IsDeleted to true
           // this.exercisesRepository.Delete(exercise);

            return this.db.SaveChanges();
        }

        public async Task<int> EditExercise(EditExerciseViewModel viewModel)
        {
            var exercise = this.exercisesRepository.All()
                 .Where(x => x.Id == viewModel.Id)
                 .FirstOrDefault();

            exercise.Name = viewModel.Name;
            exercise.Description = viewModel.Description;

            this.db.Exercises.Update(exercise);
            return await this.db.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllExercise<T>()
        {
            IQueryable<Exercise> query = this.exercisesRepository
               .All()
               .OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }

        public IEnumerable<Exercise> GetAllExercise()
        {
            return this.exercisesRepository.All().ToList();
        }

        public IEnumerable<ExerciseViewModel> GetAllExerciseViewModel()
        {
            var exercises = this.exercisesRepository.All().ToList();
            var viewModel = new List<ExerciseViewModel>();

            foreach (var exercise in exercises)
            {
                var toolsCount = this.db.Tools.Where(x => x.ExercisesTools.Any(x => x.ExerciseId == exercise.Id)).Count();

                var exerciseModel = new ExerciseViewModel
                {
                    Id = exercise.Id,
                    Name = exercise.Name,
                    Description = exercise.Description,
                    ToolsCount = toolsCount,
                };
                viewModel.Add(exerciseModel);
            }

            return viewModel
                .OrderBy(x => x.Name);
        }

        public IEnumerable<T> GetAllToolsForExercise<T>(int exerciseId)
        {
            // var tools = this.toolsService.GetAllTools<T>();

            var tools = this.db.Tools.Where(x => x.ExercisesTools.Any(x => x.ExerciseId == exerciseId));

            return tools.To<T>().ToList();
        }

        public IEnumerable<Tool> GetAllToolsForExercise(int exerciseId)
        {
            var tools = this.db.Tools.Where(x => x.ExercisesTools.Any(x => x.ExerciseId == exerciseId));

            return tools.ToList();
        }

        public T GetExerciseById<T>(int id)
        {
            var exercise = this.exercisesRepository.All()
               .Where(x => x.Id == id)
               .To<T>()
               .FirstOrDefault();

            return exercise;
        }

        public Exercise GetExerciseById(int id)
        {
            return this.exercisesRepository.All()
                 .FirstOrDefault(x => x.Id == id);
        }
    }
}
