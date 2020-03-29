namespace IDFit.Services.Data.Diets
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IDietsService
    {
        int CreateDiet(string name, DateTime startTime, DateTime endTime);

        IEnumerable<T> GetAllDiets<T>();
    }
}
