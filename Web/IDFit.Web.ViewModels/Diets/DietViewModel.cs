namespace IDFit.Web.ViewModels.Diets
{
    using System;
    using System.Collections.Generic;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Foods;

    public class DietViewModel : IMapFrom<Diet>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public virtual ICollection<FoodViewModel> Foods { get; set; }
    }
}
