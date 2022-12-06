using System.Collections.Generic;
using System.Threading.Tasks;
using ApropasTaskManager.Shared.ViewModels;

namespace ApoposTaskManager.Client.Services
{
    public interface IClientProjectService
    {
        Task<string> CreateProject(ProjectViewModel projectViewModel);
        Task<IEnumerable<ProjectViewModel>> GetProjects();
    }
}