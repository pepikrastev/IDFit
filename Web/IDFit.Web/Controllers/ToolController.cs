namespace IDFit.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IDFit.Data;
    using IDFit.Web.ViewModels.Tool;
    using Microsoft.AspNetCore.Mvc;

    public class ToolController : BaseController
    {
        private readonly ApplicationDbContext db;

        public ToolController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();

            var tools = this.db.Tools.Select(t => new IndexToolViewModel
            {
                Name = t.Name,
                Details = t.Details,
                ImageUrl = t.ImageUrl,
            })
            .ToList();

            viewModel.Tools = tools;

            return this.View(viewModel);
        }
    }
}
