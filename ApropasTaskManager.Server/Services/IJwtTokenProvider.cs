namespace ApropasTaskManager.Server.Services;

public interface IJwtTokenProvider
{
    string Generate(string login);
}
