namespace IDFit.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using IDFit.Data.Common.Models;

    public class Exercise : BaseDeletableModel<int>
    {
        public Exercise()
        {
            this.ExercisesTools = new List<ExerciseTool>();
            this.TrainingsExercises = new List<TrainingExercise>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ExerciseTool> ExercisesTools { get; set; }

        public virtual ICollection<TrainingExercise> TrainingsExercises { get; set; }
    }
}
