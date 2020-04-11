namespace IDFit.Web.ViewModels.Tools
{
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class ToolViewModel : IMapFrom<Tool>, IMapTo<Tool>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Details { get; set; }
    }
}
