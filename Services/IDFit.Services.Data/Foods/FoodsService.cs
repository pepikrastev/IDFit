namespace IDFit.Services.Data.Foods
{
    using System.Collections.Generic;
    using System.Linq;
    using IDFit.Data;
    using IDFit.Data.Common.Repositories;
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class FoodsService : IFoodsService
    {
        private readonly IDeletableEntityRepository<Food> foodsRepository;
        private readonly ApplicationDbContext db;

        public FoodsService(IDeletableEntityRepository<Food> foodsRepository, ApplicationDbContext db)
        {
            this.foodsRepository = foodsRepository;
            this.db = db;
        }

        public int EditFood(int id, string name, int quantity, double weight)
        {
            var food = this.foodsRepository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            food.Name = name;
            food.Quantity = quantity;
            food.Weight = weight;

            return this.db.SaveChanges();
        }

        public IEnumerable<T> GetAllFoods<T>()
        {
            IQueryable<Food> query = this.foodsRepository
                .All()
                .OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }

        public T GetFoodById<T>(int id)
        {
            var food = this.foodsRepository.All()
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefault();

            return food;
        }
    }
}
