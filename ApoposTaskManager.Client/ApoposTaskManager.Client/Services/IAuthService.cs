using System.Threading.Tasks;

namespace ApoposTaskManager.Client.Services
{
    public interface IAuthService
    {
        Task<bool> Login(string login, string password);
    }
}