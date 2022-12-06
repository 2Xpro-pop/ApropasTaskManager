using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared;
using ApropasTaskManager.Shared.ViewModels;
using Microsoft.AspNetCore.JsonPatch;

namespace ApoposTaskManager.Client.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Current user
        /// </summary>
        IObservable<UserViewModel> User
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
        Task<string> CreateUserAsync(UserViewModel user);

        Task<bool> UpdateUserAsync(JsonPatchDocument<UserViewModel> json);
        Task<IEnumerable<UserViewModel>> GetUsersPage(int page, int pageSize);
        Task<IEnumerable<UserViewModel>> GetUsersByName(string name);
        Task<IEnumerable<UserViewModel>> GetManagersPage(int page, int pageSize);
    }
}
