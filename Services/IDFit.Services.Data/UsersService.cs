namespace IDFit.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using IDFit.Data.Common.Repositories;
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(IDeletableEntityRepository<ApplicationUser> userRepository, UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        public IEnumerable<T> GetAllUsers<T>()
        {
            IQueryable<ApplicationUser> query = this.userRepository.All()
               .OrderBy(x => x.UserName);

            return query.To<T>().ToList();
        }

        public T GetUserById<T>(string id)
        {
            var model = this.userRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return model;
        }

        public ApplicationUser GetUserById(string id)
        {
            var user = this.userRepository.All()
                 .FirstOrDefault(x => x.Id == id);

            return user;
        }

        public T GetUserByUsername<T>(string username)
        {
            var user = this.userManager.FindByNameAsync(username);

            var model = this.userRepository.All()
                 .Where(x => x.UserName == username)
                 .To<T>()
                 .FirstOrDefault();

            return model;
        }
    }
}
