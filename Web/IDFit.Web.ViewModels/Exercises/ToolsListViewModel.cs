namespace IDFit.Web.ViewModels.Exercises
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class ToolsListViewModel : IMapFrom<Tool>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Details { get; set; }

        public bool IsSelected { get; set; }
    }
}
