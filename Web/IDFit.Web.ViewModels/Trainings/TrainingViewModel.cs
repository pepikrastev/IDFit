namespace IDFit.Web.ViewModels.Trainings
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class TrainingViewModel : IMapFrom<Training>
    {
        public string Name { get; set; }

        // in minutes
        public int TrainingTime { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
