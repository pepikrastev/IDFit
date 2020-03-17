namespace IDFit.Web.ViewModels.Tool
{
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class IndexToolViewModel : IMapFrom<Tool>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Details { get; set; }

        public string Url => $"/f/{this.Name.Replace(' ', '-')}";
    }
}