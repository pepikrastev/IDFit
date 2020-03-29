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

        public int CreateDiet(string name, DateTime startTime, DateTime endTime)
        {
            if (startTime < endTime)
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

            return -1;
        }

        public IEnumerable<T> GetAllDiets<T>()
        {
            IQueryable<Diet> query = this.dietsRepository.All();
            return query.To<T>().ToList();
        }
    }
}
