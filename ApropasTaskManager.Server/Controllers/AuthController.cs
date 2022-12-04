using System.Security.Claims;
using ApropasTaskManager.Server.Services;
using ApropasTaskManager.Shared;
using ApropasTaskManager.Shared.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApropasTaskManager.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenProvider _jwtTokenProvider;

    public AuthController(ILogger<AuthController> logger, UserManager<User> userManager, IJwtTokenProvider jwtTokenProvider)
    {
        _logger = logger;
        _userManager = userManager;
        _jwtTokenProvider = jwtTokenProvider;
    }

    //
    [HttpGet("login")]
    public async Task<IActionResult> Login(string login, string password)
    {
        var user = await _userManager.FindByNameAsync(login);

        if (user == null)
        {
            return Unauthorized();
        }

        if (await _userManager.CheckPasswordAsync(user, password))
        {
            return Ok(_jwtTokenProvider.Generate(new Claim[]
            {
                new Claim("Id", user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user))[0])
            }));
        }

        return Unauthorized();
        
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(UserRoles.Director))]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        var us = await _userManager.FindByNameAsync(user.UserName);
        if (us != null)
        {
            return BadRequest(ServerDefaultResponses.UserExist);
        }

        if (user.Role == UserRoles.Director)
        {
            return BadRequest(ServerDefaultResponses.MustBeOneDirector);
        }

        var password = Guid.NewGuid().ToString("N").Remove(5).ToUpper();
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return StatusCode(500, result.Errors);
        }

        result = await _userManager.AddToRoleAsync(user, Enum.GetName(user.Role));

        if (!result.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            return StatusCode(500, result.Errors);
        }

        return Ok(password);

    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(UserRoles.Director))]
    [HttpPost("{id}/reset-password")]
    public async Task<IActionResult> ChangePassword(string id, [FromBody] ResetPasswordViewModel resetPassword)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return BadRequest(ServerDefaultResponses.UserNotFound);
        }

        if (!resetPassword.ValidationContext.IsValid)
        {
            return BadRequest();
        }

        if (!await _userManager.CheckPasswordAsync(user, resetPassword.NewPassword))
        {
            return Unauthorized();
        }

        var result = await _userManager.ChangePasswordAsync(user, resetPassword.OldPassword, resetPassword.NewPassword);

        if (!result.Succeeded)
        {
            return StatusCode(500, result.Errors);
        }

        return Ok();
    }
}
