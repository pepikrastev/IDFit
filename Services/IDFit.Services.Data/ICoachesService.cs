﻿namespace IDFit.Services.Data
{
    using System.Collections.Generic;
    using System.Text;

    public interface ICoachesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
