namespace IDFit.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Diets;
    using IDFit.Web.ViewModels.Trainings;

    public class UserProfilViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public UserProfilViewModel()
        {
            this.Trainings = new List<TrainingViewModel>();
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

        // if user is not a coach
        public int? DietId { get; set; }

        public DietViewModel Diet { get; set; }

        public List<TrainingViewModel> Trainings { get; set; }

        public string CoachId { get; set; }

        public string CoachUserName { get; set; }

        // if user is a coach
        public List<UserWithCoachViewModel> TrainedPeople { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserProfilViewModel>()
                 .ForMember(x => x.Trainings, t => t.MapFrom(e => e.UsersTrainings.Where(x => x.UserId == e.Id).Select(x => x.Training)));
        }
    }
}
