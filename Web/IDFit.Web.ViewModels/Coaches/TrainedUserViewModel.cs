namespace IDFit.Web.ViewModels.Coaches
{
    using System.Collections.Generic;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Diets;

    public class TrainedUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public int? Age { get; set; }

        // TODO change name to PhotoPath
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public DietViewModel Diet { get; set; }

        public IEnumerable<string> TrainedPeopleUserName { get; set; }

        // if user has user
        public string CoachId { get; set; }

        public string CoachUserName { get; set; }
    }
}
