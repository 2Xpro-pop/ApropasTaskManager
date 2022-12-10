using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.BLL.DTO;
using ApropasTaskManager.DAL.Abstractions;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.BLL.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectsRepository _projects;
        private readonly IUsersRepository _users;

        public ProjectService(IUnitOfWork db)
        {
            _projects = db.Projects;
            _users = db.Users;
        }

        public async Task<Result<Unit>> CreateProjectAsync(ProjectDTO project)
        {
            var user = await _users.GetAsyncById(project.ProjectManagerId);

            if (user.Value == null)
            {
                return Result<Unit>.CreateError(ServerDefaultResponses.UserNotFound);
            }
            else if (!user)
            {
                return Result<Unit>.CreateError(user.Error);
            }


            return await _projects.CreateAsync(new Project()
            {
                Name = project.Name,
                Description = project.Description,
                Priority = project.Priority,
                ProjectManagerId = project.ProjectManagerId,
            });
        }

        public async Task<Result<ProjectDTO>> FindByIdAsync(int id)
        {
            var result = await _projects.GetAsyncById(id);

            return result.ResultOrErrorIfNull(p => new ProjectDTO(p), ServerDefaultResponses.ProjectNotFound);
        }

        public async Task<Result<IEnumerable<ProjectDTO>>> GetProjects()
        {
            var result = await _projects.GetAllAsync();

            return result.ResultOrError<IEnumerable<ProjectDTO>>(
                projects => new List<ProjectDTO>( projects.Select(p => new ProjectDTO(p)) )
            );
        }
        public async Task<Result<Unit>> PutManager(int projectId, string userId)
        {
            var project = await _projects.GetAsyncById(projectId);
            if (!project)
            {
                return project.ToError<Unit>();
            }
            if (project.Value == null)
            {
                return project.ToError<Unit>(ServerDefaultResponses.UserNotFound);
            }

            var user = await _users.GetAsyncById(userId);
            if (!user)
            {
                return user.ToError<Unit>();
            }
            if (user.Value == null)
            {
                return user.ToError<Unit>(ServerDefaultResponses.UserNotFound);
            }

            project.Value.ProjectManagerId = userId;
            
            await _projects.UpdateAsync(project);

            return Unit.Default;
        }

        public async Task<Result<Unit>> PutUser(int projectId, string userId)
        {
            var project = await _projects.GetAsyncById(projectId);
            if (!project)
            {
                return project.ToError<Unit>();
            }
            if (project.Value == null)
            {
                return project.ToError<Unit>(ServerDefaultResponses.UserNotFound);
            }

            var user = await _users.GetAsyncById(userId);
            if (!user)
            {
                return user.ToError<Unit>();
            }
            if (user.Value == null)
            {
                return user.ToError<Unit>(ServerDefaultResponses.UserNotFound);
            }

            project.Value.Users.Add(user);
            await _projects.UpdateAsync(project);

            return Unit.Default;
        }

        public async Task<Result<Unit>> UpdateProjectAsync(ProjectDTO project)
        {
            var result = await _projects.UpdateAsync(project.ToProject());
            return result.ResultOrError(u => u);
        }
    }
}
