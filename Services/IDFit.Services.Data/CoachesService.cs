namespace IDFit.Services.Data
{
    using IDFit.Common;
    using IDFit.Data;
    using IDFit.Data.Common.Repositories;
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CoachesService : ICoachesService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly ApplicationDbContext db;

        public CoachesService(IDeletableEntityRepository<ApplicationUser> userRepository, ApplicationDbContext db)
        {
            this.userRepository = userRepository;
            this.db = db;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            var role = this.db.Roles.First(x => x.Name == GlobalConstants.CoachRoleName);

            IQueryable<ApplicationUser> query = this.userRepository.All()
                .Where(x => x.Roles.Any(r => r.RoleId == role.Id))
                .OrderBy(x => x.FirstName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
