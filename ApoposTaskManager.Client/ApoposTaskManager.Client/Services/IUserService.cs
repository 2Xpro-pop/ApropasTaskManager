using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared;

namespace ApoposTaskManager.Client.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Current user
        /// </summary>
        IObservable<User> User
        {
            get;
        }

        /// <summary>
        /// if task completed succescfull, update <see cref="P:ApoposTaskManager.Client.Services.IUserService.User)" /> property
        /// </summary>
        /// <returns></returns>
        Task GetCurrentUserInfoAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returned password</returns>
        Task<string> CreateUserAsync(User user);
    }
}
