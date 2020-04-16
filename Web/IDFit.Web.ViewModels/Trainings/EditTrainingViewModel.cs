﻿namespace IDFit.Web.ViewModels.Trainings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class EditTrainingViewModel : IMapFrom<Training>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // in minutes
        [Range(10, 200)]
        public int TrainingTime { get; set; }

        public string Description { get; set; }

        public ICollection<ExercisesListViewModel> Exercises { get; set; }
    }
}
