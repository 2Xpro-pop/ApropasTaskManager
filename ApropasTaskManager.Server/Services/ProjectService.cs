using System.Reactive;
using ApropasTaskManager.DAL.Abstractions;
using ApropasTaskManager.DAL.EF;
using ApropasTaskManager.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApropasTaskManager.Server.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectsRepository _projects;
    private readonly IUsersRepository _users;

    public ProjectService(IUnitOfWork db)
    {
        _projects = db.Projects;
        _users = db.Users;
    }

    public async Task<Project> CreateProjectAsync(Project project)
    {
        await _projects.CreateAsync(project);

        return project;
    }

    public async Task<Project?> FindByIdAsync(int id)
    {
        return await _projects.GetAsyncById(id);
    }

    public async Task<Result<Unit>> PutUser(int projectId, string userId)
    {
        var project = await _projects.GetAsyncById(projectId);

        if (!project)
        {
            return Result<Unit>.CreateError(project.Error);
        }

        var user = await _users.GetAsyncById(userId);

        if (!user)
        {
            return Result<Unit>.CreateError(user.Error);
        }

        project.Value.Users.Add(user);
        await _projects.UpdateAsync(project);

        return Unit.Default;
    }

    public async Task<Result<Unit>> PutManager(int projectId, string userId)
    {
        var project = await _projects.GetAsyncById(projectId);

        if (!project)
        {
            return Result<Unit>.CreateError(project.Error);
        }

        var manager = await _users.GetAsyncById(userId);

        if (!manager)
        {
            return Result<Unit>.CreateError(manager.Error);
        }

        project.Value.ProjectManagerId = userId;
        await _projects.UpdateAsync(project);

        return Unit.Default;
    }

    public Task UpdateProjectAsync(Project project)
    {
        return _projects.UpdateAsync(project);
    }

    public async Task<List<Project>> GetProjects()
    {
        var result = await _projects.GetAllAsync();

        return await result.Value.ToListAsync();
    }
}
