namespace IDFit.Web.ViewModels.Diets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Foods;

    public class CreateDietViewModel : IMapFrom<Diet>, IMapTo<Diet>
    {
        public CreateDietViewModel()
        {
            this.Foods = new List<FoodViewModel>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }

        public int Days => (this.EndTime - this.StartTime).Days;

        public IEnumerable<FoodViewModel> Foods { get; set; }
    }
}
