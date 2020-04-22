namespace IDFit.Web.ViewModels.Tools
{
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class IndexToolViewModel : IMapFrom<Tool>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Details { get; set; }
    }
}