namespace IDFit.Services.Data.Diets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using IDFit.Data;
    using IDFit.Data.Common.Repositories;
    using IDFit.Data.Models;
    using IDFit.Services.Data.Foods;
    using IDFit.Services.Mapping;

    public class DietsService : IDietsService
    {
        private readonly IDeletableEntityRepository<Diet> dietsRepository;
        private readonly ApplicationDbContext db;
        private readonly IFoodsService foodsService;
        private readonly IUsersService usersService;

        public DietsService(IDeletableEntityRepository<Diet> dietsRepository, ApplicationDbContext db, IFoodsService foodsService, IUsersService usersService)
        {
            this.dietsRepository = dietsRepository;
            this.db = db;
            this.foodsService = foodsService;
            this.usersService = usersService;
        }

        public int EditDiet(Diet diet, string name, DateTime startTime, DateTime endTime, string description)
        {
            diet.Name = name;
            diet.StartTime = startTime;
            diet.EndTime = endTime;
            diet.Description = description;

            this.db.Diets.Update(diet);
            return this.db.SaveChanges();
        }

        public int CreateDiet(string name, DateTime startTime, DateTime endTime, string description)
        {
            var diet = new Diet
            {
                Name = name,
                StartTime = startTime,
                EndTime = endTime,
                Description = description,
            };

            this.db.Diets.Add(diet);
            return this.db.SaveChanges();
        }

        public int DeleteDiet(int id)
        {
            var diet = this.dietsRepository.All()
                .FirstOrDefault(x => x.Id == id);

            var users = this.usersService.GetAllUsers();
            foreach (var user in users)
            {
                if (user.DietId == id)
                {
                    user.DietId = null;
                   // user.Diet = null;
                    this.db.Users.Update(user);
                }
            }

            var foods = this.foodsService.GetAllFoods();
            foreach (var food in foods)
            {
                if (food.DietId == id)
                {
                    food.DietId = null;
                    // food.Diet = null;
                    this.db.Foods.Update(food);
                }
            }

            // diet.Foods = null;
            // diet.Users = null;

            this.db.Diets.Remove(diet);
            return this.db.SaveChanges();
        }

        public IEnumerable<T> GetAllDiets<T>()
        {
            var isExpiredDietsId = new List<int>();
            foreach (var diet in this.db.Diets)
            {
                if (diet.EndTime < DateTime.UtcNow)
                {
                    isExpiredDietsId.Add(diet.Id);
                }
            }

            foreach (var dietId in isExpiredDietsId)
            {
                var diet = this.db.Diets.Where(x => x.Id == dietId).FirstOrDefault();

                // delete diet from db
                // this.DeleteDiet(diet.Id);

                // diet.IsDeleted = true
                this.dietsRepository.Delete(diet);
                this.db.SaveChanges();
            }

            IQueryable<Diet> query = this.dietsRepository.All();
            return query.To<T>().ToList();
        }

        public T GetDietById<T>(int id)
        {
            var diet = this.dietsRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return diet;
        }

        public Diet GetDietById(int id)
        {
            return this.dietsRepository.All()
                .FirstOrDefault(x => x.Id == id);
        }

        public void EditDietInDb(Diet diet)
        {
            this.db.Diets.Update(diet);
            this.db.SaveChanges();
        }
    }
}
