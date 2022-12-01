using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared;

namespace ApoposTaskManager.Client.Services
{
    public interface IUserService
    {
        IObservable<User> User
        {
            get;
        }
        Task GetUserInfo();
    }
}
