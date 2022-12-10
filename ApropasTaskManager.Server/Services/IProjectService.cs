using System.Reactive;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.Server.Services;
public interface IProjectService
{
    Task<Project> CreateProjectAsync(Project project);
    Task<Project?> FindByIdAsync(int id);
    Task<List<Project>> GetProjects();
    Task<Result<Unit>> PutUser(int projectId, string userId);
    Task<Result<Unit>> PutManager(int projectId, string userId);
    Task UpdateProjectAsync(Project project);
}