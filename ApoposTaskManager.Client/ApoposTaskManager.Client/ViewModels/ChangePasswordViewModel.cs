using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ApoposTaskManager.Client.Services;
using ApropasTaskManager.Shared.ViewModels;
using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class ChangePasswordViewModel: ResetPasswordViewModel
    {
        [Reactive, JsonIgnore] public string Result { get; set; }

        [JsonIgnore]
        public ReactiveCommand<Unit, bool> ChangePasswordCommand { get; }
        public ChangePasswordViewModel():base()
        {
            ChangePasswordCommand = ReactiveCommand.CreateFromTask(
                () => DependencyService.Get<IAuthService>().ChangePassword(this), 
                ValidationContext.Valid
            );

            ChangePasswordCommand.Subscribe(result =>
            {
                Result = result ? "Success!" : "Error, maybe incorrect password"; 
            });

            ChangePasswordCommand.ThrownExceptions.Subscribe(exc =>
            {
                Result = "Failed to establish a connection with the server"; 
            });
        }
    }
}
