namespace IDFit.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Food
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        // in grams
        public double Weight { get; set; }

        public int? DietId { get; set; }

        public virtual Diet Diet { get; set; }
    }
}
