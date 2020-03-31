namespace IDFit.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using IDFit.Common;
    using IDFit.Data;
    using IDFit.Data.Common.Repositories;
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class CoachesService : ICoachesService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly ApplicationDbContext db;

        public CoachesService(IDeletableEntityRepository<ApplicationUser> userRepository, ApplicationDbContext db)
        {
            this.userRepository = userRepository;
            this.db = db;
        }

        public T GetCoachByName<T>(string name)
        {
            var user = this.userRepository.All()
                 .Where(x => x.FirstName == name)
                 .To<T>()
                 .FirstOrDefault();

            return user;
        }

        public T GetCoachById<T>(string id)
        {
            var user = this.userRepository.All()
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefault();

            return user;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<ApplicationUser> query = this.userRepository
                .All()
                .OrderBy(x => x.FirstName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllCoaches<T>()
        {
            var role = this.db.Roles.First(x => x.Name == GlobalConstants.CoachRoleName);

            IQueryable<ApplicationUser> query = this.userRepository.All()
                .Where(x => x.Roles.Any(r => r.RoleId == role.Id))
                .OrderBy(x => x.FirstName);

            return query.To<T>().ToList();
        }
    }
}
