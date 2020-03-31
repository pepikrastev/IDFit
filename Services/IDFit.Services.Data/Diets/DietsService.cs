namespace IDFit.Services.Data.Diets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using IDFit.Data;
    using IDFit.Data.Common.Repositories;
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class DietsService : IDietsService
    {
        private readonly IDeletableEntityRepository<Diet> dietsRepository;
        private readonly ApplicationDbContext db;

        public DietsService(IDeletableEntityRepository<Diet> dietsRepository, ApplicationDbContext db)
        {
            this.dietsRepository = dietsRepository;
            this.db = db;
        }

        public int AddDietInDb(Diet diet, string name, DateTime startTime, DateTime endTime)
        {
            diet.Name = name;
            diet.StartTime = startTime;
            diet.EndTime = endTime;

            return this.db.SaveChanges();
        }

        public int CreateDiet(string name, DateTime startTime, DateTime endTime)
        {
            var diet = new Diet
            {
                Name = name,
                StartTime = startTime,
                EndTime = endTime,
            };

            this.db.Diets.Add(diet);
            return this.db.SaveChanges();
        }

        public int DeleteDiet(int id)
        {
            var diet = this.dietsRepository.All()
                .FirstOrDefault(x => x.Id == id);

            this.db.Diets.Remove(diet);
            return this.db.SaveChanges();
        }

        public IEnumerable<T> GetAllDiets<T>()
        {
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

    }
}
