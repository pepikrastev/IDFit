namespace IDFit.Web.ViewModels.Exercises
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class ExerciseViewModel : IMapFrom<Exercise>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int ToolsCount { get; set; }
    }
}
