namespace IDFit.Web.ViewModels.Coaches
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class TrainigForListViewModel : IMapFrom<Training>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TrainingTime { get; set; }

        public string Description { get; set; }

        public bool IsSelected { get; set; }

        public int ExerciseCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Training, TrainigForListViewModel>()
                 .ForMember(x => x.ExerciseCount, t => t.MapFrom(e => e.TrainingsExercises.Where(x => x.TrainingId == e.Id).Count()));
        }
    }
}
