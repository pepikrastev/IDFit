namespace IDFit.Web.ViewModels.Tools
{
    using System.ComponentModel.DataAnnotations;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class ToolViewModel : IMapFrom<Tool>, IMapTo<Tool>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IFormFile Photo { get; set; }

        public string Details { get; set; }
    }
}
