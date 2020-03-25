namespace IDFit.Web.ViewModels.Administration.Administration
{
    using System.ComponentModel.DataAnnotations;

    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
