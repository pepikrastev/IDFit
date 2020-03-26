namespace IDFit.Web.ViewModels.Administration.Administration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            this.Users = new List<string>();
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
