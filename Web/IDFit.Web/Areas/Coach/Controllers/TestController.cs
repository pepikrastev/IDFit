namespace IDFit.Web.Areas.Coach.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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