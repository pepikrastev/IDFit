namespace IDFit.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Tool
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Details { get; set; }

        public int? ExerciseId { get; set; }

        public virtual Exercise Exercise { get; set; }
    }
}
