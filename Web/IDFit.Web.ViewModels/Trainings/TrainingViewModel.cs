namespace IDFit.Web.ViewModels.Trainings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class TrainingViewModel : IMapFrom<Training>, IHaveCustomMappings
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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Training, TrainingViewModel>()
                 .ForMember(x => x.ExercisesCount, t => t.MapFrom(e => e.TrainingsExercises.Where(x => x.TrainingId == e.Id).Count()));
        }
    }
}
