namespace IDFit.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using IDFit.Data.Models;

    public interface IUsersService
    {
        T GetUserByUsername<T>(string username);

        T GetUserById<T>(string id);

        IEnumerable<T> GetAllUsers<T>();

        ApplicationUser GetUserById(string id);

        IEnumerable<T> GetAllUsersWithCoach<T>(string id);

        void EditUserProperty(ApplicationUser user);

        int AddUserToCoach(string coachId, ApplicationUser user);

        int RemoveUserFromCoach(ApplicationUser user, ApplicationUser coach);
    }
}
