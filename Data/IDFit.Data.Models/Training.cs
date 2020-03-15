namespace IDFit.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Training
    {
        public Training()
        {
            this.Exercises = new List<Exercise>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        // in minutes
        public int TrainingTime { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
