namespace IDFit.Web.Areas.Administration.Controllers
{
    using IDFit.Common;
    using IDFit.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    // [Authorize(Roles = GlobalConstants.CoachRoleName)]

    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
