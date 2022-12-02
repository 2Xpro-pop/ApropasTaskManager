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
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class LoginViewModel : ReactiveObject, IValidatableViewModel
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

        public ValidationContext ValidationContext { get; } = new ValidationContext();

        public LoginViewModel()
        {
            this.ValidationRule(
                vm => vm.Login,
                login => !string.IsNullOrWhiteSpace(login),
                "The login cannot be empty!");

            this.ValidationRule(
                vm => vm.Password,
                password => password?.Length > 4,
                "The password must be greater than 4");


            LoginCommand = ReactiveCommand.CreateFromTask(
                async () =>
                {
                    var authService = DependencyService.Get<IAuthService>();

                    var result = await authService.LoginAsync(Login, Password);

                    if(result)
                    {
                        await Shell.Current.GoToAsync("//main");
                    }

                    return result;
                }, ValidationContext.Valid);
        }

    }
}
