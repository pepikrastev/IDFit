namespace IDFit.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using IDFit.Common;
    using IDFit.Data;
    using IDFit.Services.Data.Tools;
    using IDFit.Web.ViewModels.Tools;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.CoachRoleName)]
    public class ToolsController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly IToolsService toolsService;

        public ToolsController(ApplicationDbContext db, IToolsService toolsService)
        {
            this.db = db;
            this.toolsService = toolsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();

            var tools = this.db.Tools.Select(t => new ToolViewModel
            {
                Name = t.Name,
                Details = t.Details,
                ImageUrl = t.ImageUrl,
            })
            .ToList();

            viewModel.Tools = tools;

            return this.View(viewModel);
        }

        public IActionResult AllTools()
        {
            var viewModel = new List<ToolViewModel>();

            // var tools = this.db.Tools.Select(t => new ToolViewModel
            // {
            //    Name = t.Name,
            //    Details = t.Details,
            //    ImageUrl = t.ImageUrl,
            // })
            // .ToList();

            var tools = this.toolsService.GetAllTools<ToolViewModel>();

            viewModel = tools.ToList();

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateTool()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTool(ToolViewModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                await this.toolsService.CreateTool(inputModel);
                return this.Redirect("/Tools/AllTools");
            }

            return this.View(inputModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditTool(int id)
        {
            var viewModel = await this.toolsService.GetToolById<ToolViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditTool(ToolViewModel viewModel)
        {
            await this.toolsService.EditTool(viewModel.Id, viewModel.Name, viewModel.Details, viewModel.ImageUrl);

            return this.RedirectToAction("AllTools");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTool(int id)
        {
            await this.toolsService.DeleteFood(id);
            return this.RedirectToAction("AllTools");
        }
    }
}
