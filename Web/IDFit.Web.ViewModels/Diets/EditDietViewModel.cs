namespace IDFit.Web.ViewModels.Diets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Foods;

    public class EditDietViewModel : IMapFrom<Diet>, IMapTo<Diet>
    {
        public EditDietViewModel()
        {
            this.Foods = new List<FoodViewModel>();
            this.UsersUsersname = new List<string>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTime EndTime { get; set; }

        public int Days => (this.EndTime - this.StartTime).Days;

        public string Description { get; set; }

        public IEnumerable<FoodViewModel> Foods { get; set; }

        public IEnumerable<string> UsersUsersname { get; set; }
    }
}
