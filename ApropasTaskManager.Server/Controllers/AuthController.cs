using ApropasTaskManager.Server.Models;
using ApropasTaskManager.Server.Services;
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
    [HttpGet("/login")]
    public async Task<IActionResult> Login(string login, string password)
    {
        _logger.LogInformation($"user {login} tried login");
        var user = await _userManager.FindByNameAsync(login);

        if (user == null)
        {
            return Unauthorized();
        }

        if (await _userManager.CheckPasswordAsync(user, password))
        {
            return Ok(_jwtTokenProvider.Generate(login));
        }

        return Unauthorized();
        
    }
}
