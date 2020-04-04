namespace IDFit.Services.Data
{
    using IDFit.Data.Models;
    using System.Collections.Generic;
    using System.Text;

    public interface ICoachesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAllCoaches<T>();

        T GetCoachByName<T>(string name);

        T GetCoachById<T>(string id);

        int AddDiet(Diet diet, ApplicationUser user);
    }
}
