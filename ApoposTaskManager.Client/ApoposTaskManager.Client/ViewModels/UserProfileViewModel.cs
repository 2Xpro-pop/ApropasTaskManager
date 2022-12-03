using System;
using System.Collections.Generic;
using System.Text;
using ApoposTaskManager.Client.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class UserProfileViewModel: ReactiveObject
    {
        [Reactive] public string Login { get; private set; }
        [Reactive] public string Name { get; private set; }
        [Reactive] public string Suranme { get; private set; }
        [Reactive] public string Midllename { get; private set; }

        public UserProfileViewModel()
        {
            var userService = DependencyService.Get<IUserService>();

            userService.User.Subscribe(user =>
            {
                Login = user.UserName;
                Name = user.Profile.Name;
                Suranme = user.Profile.Surname;
                Midllename = user.Profile.MiddleName;
            });
        }
    }
}
