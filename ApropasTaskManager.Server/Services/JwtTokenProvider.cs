using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApropasTaskManager.Server.Services;

public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly AuthOptions _authOptions;

    public JwtTokenProvider(IOptions<AuthOptions> authOptions)
    {
        _authOptions = authOptions.Value;
    }

    public string Generate(string login)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, login) };
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
                issuer: _authOptions.Issuers.First(),
                audience: _authOptions.Audiences.First(),
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(_authOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }
}
