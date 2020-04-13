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
            this.ExercosesTools = new List<ExercoseTool>();
            this.TrainingsTools = new List<TrainingExercise>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ExercoseTool> ExercosesTools { get; set; }

        public virtual ICollection<TrainingExercise> TrainingsTools { get; set; }
    }
}
