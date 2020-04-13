namespace IDFit.Web.ViewModels.Exercises
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
        public CreateExerciseViewModel()
        {
            this.Tools = new List<ToolsListViewModel>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ToolsListViewModel> Tools { get; set; }

        public int? TrainingId { get; set; }

        public virtual Training Training { get; set; }
    }
}
