namespace IDFit.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Diet
    {
        public Diet()
        {
            this.Foods = new List<Food>();
            this.Users = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public virtual ICollection<Food> Foods { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
