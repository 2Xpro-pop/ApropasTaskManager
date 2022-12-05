using ApropasTaskManager.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApropasTaskManager.Server.Services;

public class ProjectService : IProjectService
{
    private readonly ApplicationContext _db;
    private readonly UserManager<User> _userManager;

    public ProjectService(ApplicationContext db, UserManager<User> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<Project> CreateProjectAsync(Project project)
    {
        await _db.Projects.AddAsync(project);

        return project;
    }

    public Task<Project?> FindByIdAsync(int id)
    {
        return _db.Projects.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<RequestResult> PutUser(int projectId, string userId)
    {
        var project = await FindByIdAsync(projectId);

        if (project == null)
        {
            return RequestResult.Error(ServerDefaultResponses.ProjectNotFound);
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return RequestResult.Error(ServerDefaultResponses.UserNotFound);
        }

        project.Users.Add(user);

        await _db.SaveChangesAsync();

        return RequestResult.Success();
    }
}
