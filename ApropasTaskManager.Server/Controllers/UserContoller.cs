using System.Security.Claims;
using ApropasTaskManager.Server.ViewModels;
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
    public async Task<UserViewModel> GetUser()
    {
        return new UserViewModel(await _userManager.FindByIdAsync(User.FindFirstValue("Id")));
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = nameof(UserRoles.Director))]
    public async Task<IActionResult> PatchUser(string id, [FromBody] JsonPatchDocument<UserViewModel> patch)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return BadRequest("USER_DOESN'T_EXIST");
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
            return BadRequest("USER_DOESN'T_EXIST");
        }

        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            return StatusCode(500, result.Errors);
        }

        return Ok();
    }
}
