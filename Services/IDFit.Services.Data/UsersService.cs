﻿namespace IDFit.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using IDFit.Data;
    using IDFit.Data.Common.Repositories;
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext db;

        public UsersService(IDeletableEntityRepository<ApplicationUser> userRepository, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.db = db;
        }

        public int AddUserToCoach(string coachId, ApplicationUser user)
        {
            var coach = this.userRepository.All()
                  .FirstOrDefault(x => x.Id == coachId);

            user.Coach = coach;
            user.CoachId = coachId;
            coach.TrainedPeople.ToHashSet().Add(user);

            this.db.Users.Update(user);
            this.db.Users.Update(coach);

            return this.db.SaveChanges();
        }

        public void EditUserProperty(ApplicationUser user)
        {
            var coaches = this.userRepository.All();

            foreach (var coach in coaches)
            {
                if (coach.TrainedPeople.Contains(user))
                {
                    coach.TrainedPeople.ToList().Remove(user);
                }
            }

            user.Coach = null;
            user.CoachId = null;
            user.Trainings = null;

            this.db.Users.Update(user);
            this.db.Users.UpdateRange(coaches);
            this.db.SaveChanges();
        }

        public IEnumerable<T> GetAllUsers<T>()
        {
            IQueryable<ApplicationUser> query = this.userRepository.All()
               .OrderBy(x => x.UserName);

            return query.To<T>().ToList();
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return this.userRepository.All().ToList();
        }

        public IEnumerable<T> GetAllUsersWithCoach<T>(string id)
        {

            IQueryable<ApplicationUser> query = this.userRepository.All()
                .Where(x => x.CoachId == id && !x.Roles.Any())
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

        public int RemoveUserFromCoach(ApplicationUser user, ApplicationUser coach)
        {
            // remove from user
            user.Coach = null;
            user.CoachId = null;

            // remove from coach
            if (coach.TrainedPeople.Contains(user))
            {
                coach.TrainedPeople.ToList().Remove(user);
            }

            this.db.Users.Update(user);
            this.db.Users.Update(coach);
            return this.db.SaveChanges();
        }
    }
}
