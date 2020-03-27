namespace IDFit.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using IDFit.Data.Models;

    public interface IUsersService
    {
        T GetUser<T>(string username);
    }
}
