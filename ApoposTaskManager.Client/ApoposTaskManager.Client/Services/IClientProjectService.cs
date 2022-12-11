using System.Collections.Generic;
using System.Threading.Tasks;
using ApoposTaskManager.Client.ViewModels;
using ApropasTaskManager.BLL.DTO;
using ApropasTaskManager.Shared.ViewModels;

namespace ApoposTaskManager.Client.Services
{
    public interface IClientProjectService
    {
        Task<string> CreateProject(CreateProjectViewModel projectViewModel);
        Task<IEnumerable<ProjectDTO>> GetProjects();
        Task<string> PutUser(int projectId, string userId);
        Task<string> SelectManager(int projectId, string userId);
    }
}