using System.Security.Claims;
using ApropasTaskManager.Shared;
using ApropasTaskManager.Shared.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ApropasTaskManager.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationContext _db;

    public UserController(UserManager<User> userManager, ApplicationContext db)
    {
        _userManager = userManager;
        _db = db;
    }
    [HttpGet]
    public async Task<UserViewModel> GetUser()
    {
        return new UserViewModel(await _userManager.FindByIdAsync(User.FindFirstValue("Id")));
    }

    [HttpGet("{id}")]
    public async Task<UserViewModel> GetUser(string id)
    {
        return new UserViewModel(await _userManager.FindByIdAsync(id));
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = nameof(UserRoles.Director))]
    public async Task<IActionResult> PatchUser(string id, [FromBody] JsonPatchDocument<UserViewModel> patch)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return BadRequest(ServerDefaultResponses.UserNotFound);
        }

        var userVm = new UserViewModel(user);

        patch.ApplyTo(userVm);
        userVm.ApplyToUser(user);

        var result  = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return StatusCode(500, result.Errors);
        }

        return Ok();
    }

    /// <summary>
    /// Patches self profile
    /// </summary>
    /// <param name="patch"></param>
    /// <returns></returns>
    [HttpPatch]
    public Task PatchUser([FromBody] JsonPatchDocument<UserViewModel> patch)
    {
        return PatchUser(User.FindFirstValue("Id"), patch);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = nameof(UserRoles.Director))]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return BadRequest(ServerDefaultResponses.UserNotFound);
        }

        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            return StatusCode(500, result.Errors);
        }

        return Ok();
    }

    [HttpGet("{page}/{pageSize}")]
    [Authorize(Roles = nameof(UserRoles.Director) + "," + nameof(UserRoles.ProjectManager))]
    public async Task<IActionResult> GetUsers(int page, int pageSize)
    {
        var user = await _db.Users
                            .Include(u => u.Profile)
                            .Include(u => u.Projects)
                            .OrderBy(u => u.Profile.Name)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Select(u => new UserViewModel(u))
                            .ToListAsync();
        return Ok(user);
    }

    [HttpGet("managers/{page}/{pageSize}")]
    [Authorize(Roles = nameof(UserRoles.Director) + "," + nameof(UserRoles.ProjectManager))]
    public async Task<IActionResult> GetManagers(int page, int pageSize)
    {
        var user = await _db.Users
                            .Include(u => u.Profile)
                            .Include(u => u.Projects)
                            .OrderBy(u => u.Profile.Name)
                            .Where(u => u.Role == UserRoles.ProjectManager)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Select(u => new UserViewModel(u))
                            .ToListAsync();
        return Ok(user);
    }

    [HttpGet("find-by-name")]
    [Authorize(Roles = nameof(UserRoles.Director) + "," + nameof(UserRoles.ProjectManager))]
    public async Task<IActionResult> GetUsersByName(string name)
    {
        var user = await _db.Users
                            .Include(u => u.Profile)
                            .Where(u => u.Profile.Name.Contains(name))
                            .OrderBy(u => u.Profile.Name)
                            .Select(u => new UserViewModel(u))
                            .ToListAsync();
        return Ok(user);
    }
}
