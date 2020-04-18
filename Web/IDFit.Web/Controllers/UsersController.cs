namespace IDFit.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using IDFit.Common;
    using IDFit.Data.Models;
    using IDFit.Services.Data;
    using IDFit.Web.ViewModels.Trainings;
    using IDFit.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;
        private readonly IHostingEnvironment hostingEnvironment;

        public UsersController(UserManager<ApplicationUser> userManager, IUsersService usersService, IHostingEnvironment hostingEnvironment)
        {
            this.userManager = userManager;
            this.usersService = usersService;
            this.hostingEnvironment = hostingEnvironment;
        }

        // [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.CoachRoleName}")]
        [HttpGet]
        [Authorize(Roles = "Administrator, Coach")]
        public IActionResult AllUsers()
        {
            var model = new AllUsersViewModel();
            var users = this.usersService.GetAllUsers<EditUserViewModel>();
            model.Users = users;

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {
            // var user = await this.userManager.FindByNameAsync(name);
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);

            if (user == null)
            {
                return this.RedirectToAction("Error");
            }

            // var viewModel = this.usersService.GetUser<UserProfilViewModel>(user.UserName);
            // return this.View(viewModel);
            var model = new UserProfilViewModel
            {
                Id = user.Id,
                PhotoPath = user.ImageUrl,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Description = user.Description,
                CoachId = user.CoachId,
                DietId = user.DietId,
            };

            var trainingsForUserModel = this.usersService.GetAllTrainingsForUser<TrainingViewModel>(model.Id).ToList();

            model.Trainings = trainingsForUserModel;

            if (await this.userManager.IsInRoleAsync(user, GlobalConstants.CoachRoleName))
            {
                // user it is coach
                model.CoachId = null;

                var allUsersWithThatCoach = this.usersService.GetAllUsersWithCoach<UserWithCoachViewModel>(user.Id);

                foreach (var person in allUsersWithThatCoach)
                {
                    model.TrainedPeople.Add(new UserWithCoachViewModel
                    {
                        Id = person.Id,
                        UserName = person.UserName,
                    });
                }
            }
            else
            {
                // user is not coach
                if (user.CoachId != null)
                {
                    var coach = await this.userManager.FindByIdAsync(user.CoachId);
                    model.CoachUserName = coach.UserName;
                }

                // foreach (var training in user.Trainings)
                // {
                //    model.Trainings.Add(new ViewModels.Trainings.TrainingViewModel
                //    {
                //        Name = training.Name,
                //        TrainingTime = training.TrainingTime,
                //        Description = training.Description,
                //        UserId = training.UserId,
                //    });
                // }
            }

            return this.View(model);
        }

        [HttpGet]
        public IActionResult EditUser(string id)
        {
            var model = this.usersService.GetUserById<EditUserViewModel>(id);
            var userId = this.userManager.GetUserId(this.HttpContext.User);

            if (model.Id != userId)
            {
                return this.RedirectToAction("Error");
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await this.userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                return this.RedirectToAction("Error");
            }

            if (this.ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(this.hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                user.ImageUrl = uniqueFileName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Age = model.Age;
                user.Description = model.Description;
                user.PhoneNumber = model.PhoneNumber;

                var result = await this.userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return this.RedirectToAction($"UserProfile");
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(" ", error.Description);
                }
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCoachToUser(string coachId)
        {
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);

            if (await this.userManager.IsInRoleAsync(user, GlobalConstants.CoachRoleName))
            {
                this.usersService.EditUserProperty(user);

                // TODO: massage with viewData - you are allready a coach
                return this.RedirectToAction("UserProfile");
            }

            var result = this.usersService.AddUserToCoach(coachId, user);

            if (result > -1)
            {
                return this.RedirectToAction("UserProfile");
            }

            return this.RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoachFromUser(string coachId)
        {
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            if (await this.userManager.IsInRoleAsync(user, GlobalConstants.CoachRoleName))
            {
                this.usersService.EditUserProperty(user);
                return this.RedirectToAction("UserProfile");
            }

            var coach = await this.userManager.FindByIdAsync(coachId);

            var result = this.usersService.RemoveUserFromCoach(user, coach);

            if (result > -1)
            {
                return this.RedirectToAction("UserProfile");
            }

            return this.RedirectToAction("Error");
        }

        [HttpGet]
        public IActionResult UsersDiet(string userId)
        {
            var model = this.usersService.GetUserById<UserProfilViewModel>(userId);

            return this.View(model);
        }

        [HttpGet]
        public IActionResult UserTrainingDetails(string userId, int trainingId)
        {
            var model = this.usersService.GetUserTrainingDetails(userId, trainingId);
            return this.View(model);
        }
    }
}
