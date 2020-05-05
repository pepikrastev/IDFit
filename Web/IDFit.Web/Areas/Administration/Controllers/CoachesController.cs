namespace IDFit.Web.Areas.Administration.Controllers
{
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
