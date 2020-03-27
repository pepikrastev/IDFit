namespace IDFit.Services.Data.Foods
{
    using System.Collections.Generic;

    using IDFit.Data.Models;

    public interface IFoodsService
    {
        IEnumerable<T> GetAllFoods<T>();

        T GetFoodById<T>(int id);

        public int AddFoodInDb();

        public Food GetFood(int id);
    }
}
