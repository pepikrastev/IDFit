namespace IDFit.Services.Data.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using IDFit.Web.ViewModels.Tools;

    public interface IToolsService
    {
        IEnumerable<T> GetAllTools<T>();

        Task CreateTool(ToolViewModel inputModel);

        Task<T> GetToolById<T>(int id);

        Task EditTool(int id, string name, string details, string imageUrl);

        Task DeleteFood(int id);
    }
}
