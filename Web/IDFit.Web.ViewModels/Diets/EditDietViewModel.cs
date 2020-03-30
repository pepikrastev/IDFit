﻿namespace IDFit.Web.ViewModels.Diets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using IDFit.Data.Models;
    using IDFit.Services.Mapping;

    public class EditDietViewModel : IMapFrom<Diet>, IMapTo<Diet>
    {
        public EditDietViewModel()
        {
            this.FoodsName = new List<string>();
            this.UsersUsersname = new List<string>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }

        public int Days => (this.EndTime - this.StartTime).Days;

        public IEnumerable<string> FoodsName { get; set; }

        public IEnumerable<string> UsersUsersname { get; set; }
    }
}
