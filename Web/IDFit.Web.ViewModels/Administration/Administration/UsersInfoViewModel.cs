namespace IDFit.Web.ViewModels.Administration.Administration
{
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class UsersInfoViewModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }

        public string ImageUrl { get; set; }

        public string Id { get; set; }
    }
}