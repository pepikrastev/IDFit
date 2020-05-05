namespace IDFit.Web.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using IDFit.Common;
    using IDFit.Services.Data.Tools;
    using IDFit.Web.ViewModels.Tools;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.CoachRoleName)]
    public class ToolsController : BaseController
    {
        private readonly IToolsService toolsService;
        private readonly Cloudinary cloudinary;

        public ToolsController(IToolsService toolsService, Cloudinary cloudinary)
        {
            this.toolsService = toolsService;
            this.cloudinary = cloudinary;
        }

        public IActionResult AllTools()
        {
            var viewModel = new List<IndexToolViewModel>();
            var tools = this.toolsService.GetAllTools<IndexToolViewModel>();
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
                if (inputModel.Photo != null)
                {
                    byte[] destinationImage;

                    using (var memoryStream = new MemoryStream())
                    {
                        await inputModel.Photo.CopyToAsync(memoryStream);
                        destinationImage = memoryStream.ToArray();
                    }

                    using var destinationStream = new MemoryStream(destinationImage);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(inputModel.Photo.FileName, destinationStream),
                    };

                    var resutl = await this.cloudinary.UploadAsync(uploadParams);
                    var path = resutl.Uri.AbsoluteUri;

                    await this.toolsService.CreateTool(inputModel, path);
                    return this.Redirect("/Tools/AllTools");
                }
                else
                {
                    await this.toolsService.CreateTool(inputModel, null);
                    return this.RedirectToAction("AllTools");
                }
            }

            return this.View(inputModel);
        }

        [HttpGet]
        public IActionResult EditTool(int id)
        {
            var viewModel = this.toolsService.GetToolById<ToolViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditTool(ToolViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                if (viewModel.Photo != null)
                {
                    byte[] destinationImage;

                    using (var memoryStream = new MemoryStream())
                    {
                        await viewModel.Photo.CopyToAsync(memoryStream);
                        destinationImage = memoryStream.ToArray();
                    }

                    using var destinationStream = new MemoryStream(destinationImage);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(viewModel.Photo.FileName, destinationStream),
                    };

                    var resutl = await this.cloudinary.UploadAsync(uploadParams);
                    var path = resutl.Uri.AbsoluteUri;

                    await this.toolsService.EditTool(viewModel.Id, viewModel.Name, viewModel.Details, path);

                    return this.RedirectToAction("AllTools");
                }
                else
                {
                    await this.toolsService.EditTool(viewModel.Id, viewModel.Name, viewModel.Details, null);

                    return this.RedirectToAction("AllTools");
                }
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTool(int id)
        {
            // TODO: delete from cloudinary
            await this.toolsService.DeleteTool(id);
            return this.RedirectToAction("AllTools");
        }
    }
}
