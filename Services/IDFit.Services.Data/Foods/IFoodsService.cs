namespace IDFit.Services.Data.Foods
{
    using System.Collections.Generic;

    using IDFit.Data.Models;

    public interface IFoodsService
    {
        IEnumerable<T> GetAllFoods<T>();

        T GetFoodById<T>(int id);

        int EditFood(int id, string name, int quantity, double weight);

        int CreateFood(string name, int quantity, double weight);

        int DeleteFood(int id);

        Food GetFoodById(int id);
    }
}
