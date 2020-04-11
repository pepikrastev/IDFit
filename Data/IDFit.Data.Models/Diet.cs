namespace IDFit.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using IDFit.Data.Common.Models;

    public class Diet : BaseDeletableModel<int>
    {
        public Diet()
        {
            this.Foods = new List<Food>();
            this.Users = new HashSet<ApplicationUser>();
        }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Food> Foods { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
