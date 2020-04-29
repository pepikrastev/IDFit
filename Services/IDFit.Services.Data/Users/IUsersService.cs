namespace IDFit.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using IDFit.Data.Models;
    using IDFit.Web.ViewModels.Users;

    public interface IUsersService
    {
        T GetUserByUsername<T>(string username);

        T GetUserById<T>(string id);

        IEnumerable<T> GetAllUsers<T>();

        IEnumerable<T> GetAllTrainingsForUser<T>(string userId);

        UserTrainingDetails GetUserTrainingDetails(string userId, int trainingId);

        IEnumerable<ApplicationUser> GetAllUsers();

        ApplicationUser GetUserById(string id);

        IEnumerable<T> GetAllUsersWithCoach<T>(string id);

        void EditUserProperty(ApplicationUser user);

        int AddUserToCoach(string coachId, ApplicationUser user);

        int RemoveUserFromCoach(ApplicationUser user, ApplicationUser coach);
    }
}
