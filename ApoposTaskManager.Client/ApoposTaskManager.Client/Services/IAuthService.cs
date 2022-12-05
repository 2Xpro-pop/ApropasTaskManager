using System.Threading.Tasks;
using ApropasTaskManager.Shared.ViewModels;

namespace ApoposTaskManager.Client.Services
{
    public interface IAuthService
    {
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>returns <c>true</c> if succesed status code</returns>
        Task<bool> LoginAsync(string login, string password);

        Task<bool> ChangePassword(ResetPasswordViewModel resetPassword);
    }
}