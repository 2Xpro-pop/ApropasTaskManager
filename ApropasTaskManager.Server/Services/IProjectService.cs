using ApropasTaskManager.Shared;

namespace ApropasTaskManager.Server.Services;
public interface IProjectService
{
    Task<Project> CreateProjectAsync(Project project);
    Task<Project?> FindByIdAsync(int id);
    Task<List<Project>> GetProjects();
    Task<RequestResult> PutUser(int projectId, string userId);
    Task UpdateProjectAsync(Project project);
}