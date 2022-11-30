using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ApoposTaskManager.Client.Services;
using ApoposTaskManager.Client.Views;
using Microsoft.AspNetCore.Http.Extensions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class LoginViewModel : ReactiveObject
    {
        [Reactive]
        public string Login
        {
            get;
            set;
        }
        [Reactive]
        public string Password
        {
            get;
            set;
        }

        public ReactiveCommand<Unit, bool> LoginCommand
        {
            get;
        }

        public LoginViewModel()
        {
            var validation = this.WhenAnyValue(vm => vm.Login, vm => vm.Password, (login, password) =>
            {
                return !string.IsNullOrWhiteSpace(login)
                       && !string.IsNullOrEmpty(password)
                       && password.Length > 8;
            });

            LoginCommand = ReactiveCommand.CreateFromTask(
                async () =>
                {
                    var authService = DependencyService.Get<IAuthService>();

                    var result = await authService.Login(Login, Password);

                    if(result)
                    {
                        await Shell.Current.GoToAsync("//main");
                    }

                    return result;
                },
                validation
            );
        }

    }
}
