namespace IDFit.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Exercise
    {
        public Exercise()
        {
            this.Tools = new List<Tool>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Tool> Tools { get; set; }

        public int? TrainingId { get; set; }

        public virtual Training Training { get; set; }
    }
}
