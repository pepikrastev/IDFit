namespace IDFit.Web.ViewModels.Users
{
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EditUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public int? Age { get; set; }

        public IFormFile Photo { get; set; }

        public string Description { get; set; }
    }
}
