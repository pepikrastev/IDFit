namespace IDFit.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using IDFit.Data.Common.Models;

    public class Exercise : BaseDeletableModel<int>
    {
        public Exercise()
        {
            this.Tools = new List<Tool>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Tool> Tools { get; set; }

        public int? TrainingId { get; set; }

        public virtual Training Training { get; set; }
    }
}
