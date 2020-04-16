namespace IDFit.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using IDFit.Data.Common.Models;

    public class Training : BaseDeletableModel<int>
    {
        public Training()
        {
            this.TrainingsExercises = new List<TrainingExercise>();
            this.UsersTrainings = new List<UserTraining>();
        }

        [Required] // for next update
        public string Name { get; set; }

        // in minutes
        [Range(10, 200)] // for next update
        public int TrainingTime { get; set; }

        public string Description { get; set; }

        public virtual ICollection<UserTraining> UsersTrainings { get; set; }

        public virtual ICollection<TrainingExercise> TrainingsExercises { get; set; }
    }
}
