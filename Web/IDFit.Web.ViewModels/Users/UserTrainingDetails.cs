namespace IDFit.Web.ViewModels.Users
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Exercises;
    using IDFit.Web.ViewModels.Trainings;

    public class UserTrainingDetails : IMapFrom<Training>, IHaveCustomMappings
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public int TrainingTime { get; set; }

        public string Description { get; set; }

        public ICollection<ExerciseForListViewModel> Exercises { get; set; }

        // TODO: to start using them
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Training, UserTrainingDetails>()
                 .ForMember(x => x.Exercises, t => t.MapFrom(e => e.TrainingsExercises.Where(x => x.TrainingId == e.Id)));

            configuration.CreateMap<Exercise, ExerciseForListViewModel>()
                 .ForMember(x => x.ToolsCount, t => t.MapFrom(e => e.ExercisesTools.Where(x => x.ExerciseId == e.Id)));

            configuration.CreateMap<ApplicationUser, UserProfilViewModel>()
                 .ForMember(x => x.UserName, t => t.MapFrom(e => e.UserName));
        }
    }
}
