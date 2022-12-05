using ApropasTaskManager.Server.Services;
using ApropasTaskManager.Shared;
using ApropasTaskManager.Shared.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApropasTaskManager.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(JwtBearerDefaults.AuthenticationScheme)]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost("{name}")]
    [Authorize(Roles = nameof(UserRoles.Director))]
    public async Task<IActionResult> CreateProject(string name)
    {
        var project = new Project()
        {
            Name = name
        };

        await _projectService.CreateProjectAsync(project);

        return Ok(project.Id);
    }

    [HttpPut("{id}/{userId}")]
    [Authorize(Roles = nameof(UserRoles.Director) + "," + nameof(UserRoles.ProjectManager))]
    public async Task<IActionResult> PutUser(int id, string userId)
    {
        if (await _projectService.PutUser(id, userId))
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        var project = await _projectService.FindByIdAsync(id);

        if (project == null)
        {
            return BadRequest(ServerDefaultResponses.ProjectNotFound);
        }

        return Ok(new ProjectViewModel
        {
            Name = project.Name,
            Description = project.Description,
            Missions = project.Missions.Select(m => m.Id),
            Users = project.Users.Select(u => u.Id)
        });
    }
}
