namespace IDFit.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IDFit.Common;
    using IDFit.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.CoachRoleName)]
    public class CoachesController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
