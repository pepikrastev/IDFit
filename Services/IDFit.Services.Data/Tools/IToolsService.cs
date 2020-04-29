namespace IDFit.Services.Data.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using IDFit.Data.Models;
    using IDFit.Web.ViewModels.Tools;

    public interface IToolsService
    {
        IEnumerable<T> GetAllTools<T>();

        IEnumerable<Tool> GetAllTools();

        Task CreateTool(ToolViewModel inputModel, string path);

        T GetToolById<T>(int id);

        Tool GetToolById(int id);

        Task EditTool(int id, string name, string details, string imageUrl);

        Task DeleteTool(int id);
    }
}
