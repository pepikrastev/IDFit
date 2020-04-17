namespace IDFit.Services.Data.Exercises
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using IDFit.Data.Models;
    using IDFit.Web.ViewModels.Exercises;

    public interface IExercisesService
    {
        Task CreateExercise(CreateExerciseViewModel inputModel);

        IEnumerable<T> GetAllExercise<T>();

        IEnumerable<Exercise> GetAllExercise();

        IEnumerable<ExerciseViewModel> GetAllExerciseViewModel();

        int DeleteExercise(int id);

        T GetExerciseById<T>(int id);

        Exercise GetExerciseById(int id);

        IEnumerable<T> GetAllToolsForExercise<T>(int exerciseId);

        IEnumerable<Tool> GetAllToolsForExercise(int exerciseId);

        Task<int> EditExercise(EditExerciseViewModel viewModel);
    }
}
