using System.Collections.Generic;
using System.Threading.Tasks;
using ApropasTaskManager.Shared.ViewModels;

namespace ApoposTaskManager.Client.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectViewModel>> GetProjects();
    }
}