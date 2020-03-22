namespace IDFit.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IDFit.Common;
    using IDFit.Data;
    using IDFit.Data.Common.Repositories;
    using IDFit.Data.Models;
    using IDFit.Services.Data;
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Coaches;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CoachesController : Controller
    {
        private readonly ICoachesService coachesService;

        public CoachesController( ICoachesService coachesService)
        {
            this.coachesService = coachesService;
        }

        [Authorize]
        public IActionResult All()
        {
            var viewModel = new AllCoachesViewModel();
            var coaches = this.coachesService.GetAll<CoachViewModel>();
            viewModel.Coaches = coaches;

            return this.View(viewModel);
        }
    }
}
