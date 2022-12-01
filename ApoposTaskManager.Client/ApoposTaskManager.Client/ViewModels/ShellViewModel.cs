using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using ApoposTaskManager.Client.Services;
using ApropasTaskManager.Shared;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class ShellViewModel: ReactiveObject
    {
        [Reactive]
        public bool IsDirector { get; set; }

        public ShellViewModel()
        {
            var userService = DependencyService.Get<IUserService>();

            userService.User.Subscribe(user =>
            {
                IsDirector = user.Role == UserRoles.Director;
            });
        }
    }
}
