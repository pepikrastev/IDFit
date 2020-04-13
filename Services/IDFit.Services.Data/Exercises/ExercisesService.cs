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
    using IDFit.Web.ViewModels.Exercises;

    public class ExercisesService : IExercisesService
    {
        private readonly IDeletableEntityRepository<Exercise> exercisesRepository;
        private readonly ApplicationDbContext db;

        public ExercisesService(IDeletableEntityRepository<Exercise> exercisesRepository, ApplicationDbContext db)
        {
            this.exercisesRepository = exercisesRepository;
            this.db = db;
        }

        public async Task CreateExercise(CreateExerciseViewModel inputModel)
        {
            var exercise = new Exercise
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
            };

            foreach (var model in inputModel.Tools)
            {
                if (model.IsSelected)
                {
                }
            }

            await this.db.Exercises.AddAsync(exercise);
            await this.db.SaveChangesAsync();
        }
    }
}
