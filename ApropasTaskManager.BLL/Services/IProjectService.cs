using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.BLL.DTO;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.BLL.Services
{
    public interface IProjectService
    {
        /// <summary>
        /// Users and Missions will be ignore
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        Task<Result<Unit>> CreateProjectAsync(ProjectDTO project);
        Task<Result<ProjectDTO>> FindByIdAsync(int id);
        Task<Result<IEnumerable<ProjectDTO>>> GetProjects();
        Task<Result<Unit>> PutUser(int projectId, string userId);
        Task<Result<Unit>> PutManager(int projectId, string userId);
        Task<Result<Unit>> UpdateProjectAsync(ProjectDTO project);
    }
}
