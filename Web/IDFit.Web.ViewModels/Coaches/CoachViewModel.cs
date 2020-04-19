namespace IDFit.Web.ViewModels.Coaches
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class CoachViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public string Description { get; set; }

        // change to PhotoPath
        public string ImageUrl { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        public string CreatedOn { get; set; }

        public string Url => $"/t/{this.FirstName.Replace(' ', '-')}";
    }
}
