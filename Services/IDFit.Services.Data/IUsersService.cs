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
    }
}
