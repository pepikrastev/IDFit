namespace IDFit.Web.ViewModels.Trainings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class CreateTrainingViewModel : IMapFrom<Training>
    {
        [Required]
        public string Name { get; set; }

        // in minutes
        [Range(10, 200)]
        [DisplayName("Training Time")]
        public int TrainingTime { get; set; }

        public string Description { get; set; }
    }
}
