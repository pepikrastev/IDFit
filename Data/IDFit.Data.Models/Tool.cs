namespace IDFit.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using IDFit.Data.Common.Models;

    public class Tool : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Details { get; set; }

        public int? ExerciseId { get; set; }

        public virtual Exercise Exercise { get; set; }
    }
}
