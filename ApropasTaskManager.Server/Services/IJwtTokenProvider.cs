using System.Security.Claims;

namespace ApropasTaskManager.Server.Services;

public interface IJwtTokenProvider
{
    string Generate(IEnumerable<Claim> claims);
}
