namespace IDFit.Services.Data.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using IDFit.Data;
    using IDFit.Data.Common.Repositories;
    using IDFit.Data.Models;
    using IDFit.Services.Mapping;
    using IDFit.Web.ViewModels.Tools;

    public class ToolsService : IToolsService
    {
        // private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Tool> toolsRepository;
        private readonly ApplicationDbContext db;

        public ToolsService(IDeletableEntityRepository<Tool> toolsRepository, ApplicationDbContext db/*, IMapper mapper*/)
        {
            // this.mapper = mapper;
            this.toolsRepository = toolsRepository;
            this.db = db;
        }

        public async Task CreateTool(ToolViewModel inputModel)
        {
            // var tool = this.mapper.Map<Tool>(inputModel);
            var tool = new Tool
            {
                Name = inputModel.Name,
                Details = inputModel.Details,
                ImageUrl = inputModel.ImageUrl,
            };

            await this.db.Tools.AddAsync(tool);
            await this.db.SaveChangesAsync();
        }

        public async Task DeleteTool(int id)
        {
            var tool = this.toolsRepository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            // TODO: fix it
            if (tool == null)
            {
                throw new Exception("there is no tool with this id");
            }

            this.toolsRepository.Delete(tool);
            await this.db.SaveChangesAsync();
        }

        public async Task EditTool(int id, string name, string details, string imageUrl)
        {
            var tool = this.toolsRepository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            tool.Name = name;
            tool.Details = details;
            tool.ImageUrl = imageUrl;

            await this.db.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllTools<T>()
        {
            IQueryable<Tool> query = this.toolsRepository
                .All()
                .OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }

        public IEnumerable<Tool> GetAllTools()
        {
            return this.toolsRepository.All().ToList();
        }

        public async Task<T> GetToolById<T>(int id)
        {
            var tool = this.toolsRepository.All()
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefault();

            return tool;
        }

        public Tool GetToolById(int id)
        {
            var tool = this.toolsRepository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return tool;
        }
    }
}
