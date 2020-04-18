namespace IDFit.Services.Data
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using IDFit.Data.Models;
    using IDFit.Web.ViewModels.Coaches;

    public interface ICoachesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAllCoaches<T>();

        T GetCoachByName<T>(string name);

        T GetCoachById<T>(string id);

        int AddDiet(Diet diet, ApplicationUser user);

        Task<int> RemoveDietAsync(Diet diet, ApplicationUser user);

        IEnumerable<TrainigForListViewModel> GetListOfTrainigsForUser(string userId);

        Task<int> AddOrRemoveTrainingFromUserAsync(string userId, List<TrainigForListViewModel> viewModels);
    }
}
