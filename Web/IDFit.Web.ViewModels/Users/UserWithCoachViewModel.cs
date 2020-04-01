namespace IDFit.Web.ViewModels.Users
{
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class UserWithCoachViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}
