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

            for (int i = 0; i < inputModel.Tools.Count; i++)
            {
                if (inputModel.Tools[i].IsSelected)
                {
                    var toolId = inputModel.Tools[i].Id;
                    var tool = this.toolsService.GetToolById(toolId);
                    ExerciseTool exerciseTool = new ExerciseTool
                    {
                        ExerciseId = exercise.Id,
                        Exercise = exercise,
                        ToolId = toolId,
                        Tool = tool,
                    };
                    this.db.ExercisesTools.Add(exerciseTool);

                    var currentExerciseTool = this.db.ExercisesTools.Where(x => x.ExerciseId == exercise.Id && x.ToolId == tool.Id).FirstOrDefault();

                    exercise.ExercisesTools.Add(currentExerciseTool);
                    tool.ExercisesTools.Add(currentExerciseTool);
                }
            }

            this.db.Exercises.Update(exercise);
            this.db.Exercises.Update(exercise);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllExercise<T>()
        {
            IQueryable<Exercise> query = this.exercisesRepository
               .All()
               .OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }

        public IEnumerable<ExerciseViewModel> GetAllExercise()
        {
            var exercises = this.exercisesRepository.All().ToList();
            var model = new List<ExerciseViewModel>();
            foreach (var exercise in exercises)
            {
                var viewModel = new ExerciseViewModel
                {
                    Id = exercise.Id,
                    Name = exercise.Name,
                    Description = exercise.Description,
                    ToolsCount = exercise.ExercisesTools.Count(),
                };
                model.Add(viewModel);
            }

            return model;
        }
    }
}
