namespace IDFit.Web.ViewModels.Coaches
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using AutoMapper;
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Diets;
    using IDFit.Web.ViewModels.Trainings;

    public class TrainedUserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string UserName { get; set; }

        public int? Age { get; set; }

        // TODO change name to PhotoPath
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        // if user is coach
        public IEnumerable<string> TrainedPeopleUserName { get; set; }

        // if user has coach
        public DietViewModel Diet { get; set; }

        public string CoachId { get; set; }

        public string CoachUserName { get; set; }

        public IEnumerable<TrainingViewModel> Trainings { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, TrainedUserViewModel>()
                 .ForMember(x => x.Trainings, t => t.MapFrom(e => e.UsersTrainings.Where(x => x.UserId == e.Id).Select(x => x.Training)));
        }
    }
}
