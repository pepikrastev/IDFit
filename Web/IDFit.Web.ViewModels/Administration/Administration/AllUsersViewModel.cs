namespace IDFit.Web.ViewModels.Administration.Administration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class AllUsersViewModel
    {
        public IEnumerable<UsersInfoViewModel> Users { get; set; }
    }
}
