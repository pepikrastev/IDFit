﻿namespace IDFit.Services.Data.Diets
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using IDFit.Data.Models;

    public interface IDietsService
    {
        int CreateDiet(string name, DateTime startTime, DateTime endTime);

        IEnumerable<T> GetAllDiets<T>();

        int DeleteDiet(int id);

        T GetDietById<T>(int id);

        Diet GetDietById(int id);

        int AddDietInDb(Diet diet, string name, DateTime startTime, DateTime endTime);

        int DbSaveChanges();
    }
}
