using System.Security.Claims;
using ApropasTaskManager.Server.Models;
using ApropasTaskManager.Server.Services;
using ApropasTaskManager.Shared;
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

    [Authorize(AuthenticationSchemes = "Bearer", Roles = nameof(UserRoles.Director))]
    [HttpPut("register")]
    public async Task<IActionResult> Register(string login, string password, string role)
    {
        var user = await _userManager.FindByNameAsync(login);

        if (user != null)
        {
            return BadRequest("USER_EXIST");
        }

        if (!Enum.GetNames<UserRoles>().Contains(role))
        {
            return BadRequest("BAD_ROLE");
        }

        if (role == Enum.GetName(UserRoles.Director))
        {
            return BadRequest("ONLY_ONE_DIRECTOR");
        }

        user = new User
        {
            UserName = login
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return StatusCode(500);
        }

        await _userManager.AddToRoleAsync(user, role);

        return Ok();

    }
}
