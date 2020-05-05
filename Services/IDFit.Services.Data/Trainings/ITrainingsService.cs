namespace IDFit.Services.Data.Trainings
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using IDFit.Data.Models;
    using IDFit.Web.ViewModels.Trainings;

    public interface ITrainingsService
    {
        IEnumerable<TrainingViewModel> GetAllTrainingsViewModel();

        IEnumerable<Training> GetAllTrainings();

        Task CreateTraining(CreateTrainingViewModel inputModel);

        int DeleteTraining(int id);

        Training GetTrainingById(int id);

        IEnumerable<T> GetAllExerciseForTraining<T>(int trainingId);

        Task<int> EditTraining(EditTrainingViewModel viewModel);

        IEnumerable<ExerciseForListViewModel> GetExrciseListForTraining(int trainingId);

        Task<int> AddOrRemoveExerciseForTrainingAsync(int trainingId, List<ExerciseForListViewModel> viewModels);
    }
}
