using System.Threading.Tasks;

namespace ApoposTaskManager.Client.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>returns true if succesed status code</returns>
        Task<bool> LoginAsync(string login, string password);
    }
}