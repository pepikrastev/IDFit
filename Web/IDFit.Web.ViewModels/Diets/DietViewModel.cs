namespace IDFit.Web.ViewModels.Diets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Foods;

    public class DietViewModel : IMapFrom<Diet>
    {
        public DietViewModel()
        {
            this.Foods = new List<FoodViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("Start Date")]
        public DateTime StartTime { get; set; }

        [DisplayName("End Date")]
        public DateTime EndTime { get; set; }

        public string Description { get; set; }

        public ICollection<FoodViewModel> Foods { get; set; }
    }
}
