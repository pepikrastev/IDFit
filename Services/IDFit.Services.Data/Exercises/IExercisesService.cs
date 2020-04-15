namespace IDFit.Services.Data.Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using IDFit.Data.Models;
    using IDFit.Web.ViewModels.Exercises;

    public interface IExercisesService
    {
        Task CreateExercise(CreateExerciseViewModel inputModel);

        IEnumerable<T> GetAllExercise<T>();

        IEnumerable<ExerciseViewModel> GetAllExercise();

        int DeleteExercise(int id);

        T GetExerciseById<T>(int id);

        Exercise GetExerciseById (int id);

        IEnumerable<T> GetAllToolsForExercise<T>(int exerciseId);

        IEnumerable<Tool> GetAllToolsForExercise(int exerciseId);

       Task<int> EditExercise(EditExerciseViewModel viewModel);
    }
}
