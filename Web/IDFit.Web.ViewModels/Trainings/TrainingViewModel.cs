﻿namespace IDFit.Web.ViewModels.Trainings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class TrainingViewModel : IMapFrom<Training>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // in minutes
        [Range(10, 200)]
        public int TrainingTime { get; set; }

        public string Description { get; set; }

        public int UsersCount { get; set; }

        public int ExercisesCount { get; set; }
    }
}
