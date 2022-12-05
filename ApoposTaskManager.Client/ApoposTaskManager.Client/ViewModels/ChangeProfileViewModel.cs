using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ApoposTaskManager.Client.Services;
using ApropasTaskManager.Shared;
using Microsoft.AspNetCore.JsonPatch;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class ChangeProfileViewModel: ReactiveObject
    {
        [Reactive] public string Result { get; set; }
        [Reactive] public string Name { get; set; }
        [Reactive] public string Surname { get; set; }
        [Reactive] public string Middlename { get; set; }

        public ReactiveCommand<Unit, bool> ChangeProfileCommand { get; set; }

        public ChangeProfileViewModel()
        {
            var userService = DependencyService.Get<IUserService>();

            userService.User.Subscribe(user =>
            {
                Name = user.Profile.Name;
                Surname = user.Profile.Surname;
                Middlename = user.Profile.MiddleName;
            });

            ChangeProfileCommand = ReactiveCommand.CreateFromTask(() =>
            {
                var patch = new JsonPatchDocument<User>();

                patch.Replace(user => user.Profile.Name, Name);
                patch.Replace(user => user.Profile.Surname, Surname);
                patch.Replace(user => user.Profile.MiddleName, Middlename);

                return userService.UpdateUserAsync(patch);
            });

            ChangeProfileCommand.Subscribe(result =>
            {
                Result = result ? "Success!" : "Error, maybe incorrect password";
            });

            ChangeProfileCommand.ThrownExceptions.Subscribe(exc =>
            {
                Result = "Failed to establish a connection with the server";
            });
        }
    }
}
