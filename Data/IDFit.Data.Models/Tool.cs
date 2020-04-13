namespace IDFit.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using IDFit.Data.Common.Models;

    public class Tool : BaseDeletableModel<int>
    {
        public Tool()
        {
           this.ExercosesTools = new List<ExercoseTool>();
        }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Details { get; set; }

        public virtual ICollection<ExercoseTool> ExercosesTools { get; set; }
    }
}
