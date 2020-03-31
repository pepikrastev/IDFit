﻿namespace IDFit.Services.Data
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

        int AddCoach(string coachId, ApplicationUser user);
    }
}
