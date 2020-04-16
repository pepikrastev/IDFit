namespace IDFit.Web.ViewModels.Trainings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class ExercisesListViewModel : IMapFrom<Exercise>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int ToolsCount { get; set; }

        public bool IsSelected { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Exercise, ExercisesListViewModel>()
                 .ForMember(x => x.ToolsCount, t => t.MapFrom(e => e.ExercisesTools.Where(x => x.ExerciseId == e.Id).Count()));
        }
    }
}
