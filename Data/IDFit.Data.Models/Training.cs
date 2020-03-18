namespace IDFit.Data.Models
{
    using System.Collections.Generic;

    using IDFit.Data.Common.Models;

    public class Training : BaseDeletableModel<int>
    {
        public Training()
        {
            this.Exercises = new List<Exercise>();
        }

        public string Name { get; set; }

        // in minutes
        public int TrainingTime { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
