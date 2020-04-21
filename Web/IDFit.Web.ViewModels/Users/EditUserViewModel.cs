namespace IDFit.Web.ViewModels.Users
{
    using System.ComponentModel;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EditUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string UserName { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        public int? Age { get; set; }

        public IFormFile Photo { get; set; }

        public string Description { get; set; }

        public int? DietId { get; set; }

    }
}
