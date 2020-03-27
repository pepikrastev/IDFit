namespace IDFit.Web.ViewModels.Foods
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class FoodViewModel : IMapFrom<Food>, IMapTo<Food>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        // in grams
        public double Weight { get; set; }

        public int? DietId { get; set; }
    }
}
