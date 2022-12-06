﻿using System.Text.Json;
using ApropasTaskManager.Server.Services;
using ApropasTaskManager.Shared;
using ApropasTaskManager.Shared.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ApropasTaskManager.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserRoles.Director))]
    public async Task<IActionResult> CreateProject([FromBody] ProjectViewModel projectViewModel)
    {
        var project = new Project();
        projectViewModel.ApplyTo(project);

        await _projectService.CreateProjectAsync(project);

        return Ok(project.Id);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchProject(int id, [FromBody] JsonPatchDocument<ProjectViewModel> patchDocument)
    {
        var project = await _projectService.FindByIdAsync(id);

        if (project == null)
        {
            return BadRequest(ServerDefaultResponses.ProjectNotFound);
        }

        var projectVm = new ProjectViewModel(project);

        patchDocument.ApplyTo(projectVm);
        projectVm.ApplyTo(project);

        await _projectService.UpdateProjectAsync(project);

        return Ok();
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

        return Ok(new ProjectViewModel(project));
    }

    [HttpGet]
    public async Task<IEnumerable<ProjectViewModel>> GetProjects()
    {
        var projects = await _projectService.GetProjects();

        return projects.Select(p => new ProjectViewModel(p));
    }
}