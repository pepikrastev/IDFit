namespace IDFit.Web.Areas.Coach.Controllers
{
    using IDFit.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;

    [Area("Coach")]
    public class TestController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
