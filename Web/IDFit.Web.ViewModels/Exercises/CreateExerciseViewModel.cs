﻿namespace IDFit.Web.ViewModels.Exercises
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Tools;

    public class CreateExerciseViewModel : IMapFrom<Exercise>
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
