using System;
using System.Collections.Generic;
using System.Data;
using System.Reactive;
using System.Text;
using System.Windows.Input;
using ApoposTaskManager.Client.Services;
using ApoposTaskManager.Client.Views;
using ApropasTaskManager.Shared;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class UserProfileViewModel: ReactiveObject
    {
        [Reactive] public string Login { get; private set; }
        [Reactive] public UserRoles Role { get; private set; }
        [Reactive] public string Name { get; private set; }
        [Reactive] public string Suranme { get; private set; }
        [Reactive] public string Midllename { get; private set; }

        public ReactiveCommand<Unit, Unit> GotToChangePasswordCommand { get; }
        public ReactiveCommand<Unit, Unit> GotToChangeProfileCommand { get; }

        public UserProfileViewModel()
        {
            var userService = DependencyService.Get<IUserService>();

            userService.User.Subscribe(user =>
            {
                Login = user.UserName;
                Role = user.Role;
                Name = user.Profile.Name;
                Suranme = user.Profile.Surname;
                Midllename = user.Profile.MiddleName;
            });

            GotToChangePasswordCommand = ReactiveCommand.CreateFromTask(() => Shell.Current.GoToAsync(nameof(ChangePasswordPage)));
            GotToChangeProfileCommand = ReactiveCommand.CreateFromTask(() => Shell.Current.GoToAsync(nameof(ChangeProfilePage)));
        }
    }
}
