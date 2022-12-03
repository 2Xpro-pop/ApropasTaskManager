using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using ApropasTaskManager.Server.Services;
using ApropasTaskManager.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ApropasTaskManager.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public UserController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    [HttpGet]
    public Task<User> GetUser()
    {
        return _userManager.FindByIdAsync(User.FindFirstValue("Id"));
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = nameof(UserRoles.Director))]
    public async Task PatchUser(string id, [FromBody] JsonPatchDocument<User> patch)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (patch.Operations.FirstOrDefault(op => op.path.Contains("password") || op.path.Contains("userName")) != null)
        {
        
        }

        patch.ApplyTo(user);

        await _userManager.UpdateAsync(user);
    }

    /// <summary>
    /// Patches self profile
    /// </summary>
    /// <param name="patch"></param>
    /// <returns></returns>
    [HttpPatch]
    public Task PatchUser([FromBody] JsonPatchDocument<User> patch)
    {
        return PatchUser(User.FindFirstValue("Id"), patch);
    }
}
