namespace IDFit.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class UserProfilViewModel : IMapFrom<ApplicationUser>
    {
        public UserProfilViewModel()
        {
            this.Trainings = new List<string>();
            this.TrainedPeople = new List<UserWithCoachViewModel>();
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public int? Age { get; set; }

        public string PhotoPath { get; set; }

        public string Description { get; set; }

        public int? DietId { get; set; }

        public List<string> Trainings { get; set; }

        public string CoachId { get; set; }

        public string CoachUserName { get; set; }

        public List<UserWithCoachViewModel> TrainedPeople { get; set; }
    }
}
