using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;
using ApoposTaskManager.Client.Services;
using ApropasTaskManager.Shared;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class AddPersonalViewModel: ReactiveObject, IValidatableViewModel
    {
        [Reactive] public string Login { get; set; }
        [Reactive] public string Name { get; set; }
        [Reactive] public string Surname { get; set; }
        [Reactive] public string Middlename { get; set; }
        [ObservableAsProperty] public string Password { get; }
        public ReactiveCommand<Unit, string> AddPersonalCommand { get; } 
        public ICommand CancelCommand { get; }

        public ValidationContext ValidationContext { get; } = new ValidationContext();

        public AddPersonalViewModel()
        {

            this.ValidationRule(
                vm => vm.Login, 
                login => !string.IsNullOrEmpty(login) && !login.Contains(" "),
                "The login can't contains space & can't be empty");

            this.ValidationRule(
                vm => vm.Name,
                login => !string.IsNullOrEmpty(login),
                "The name can't be empty");

            this.ValidationRule(
                vm => vm.Surname,
                login => !string.IsNullOrEmpty(login),
                "The surname can't be empty");

            this.ValidationRule(
                vm => vm.Middlename,
                login => !string.IsNullOrEmpty(login),
                "The middlename can't be empty");

            AddPersonalCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var userService = DependencyService.Get<IUserService>();

                var password = await userService.CreateUserAsync(new User
                {
                    UserName = Login,
                    Name = Name,
                    Surname = Surname,
                    MiddleName = Middlename,
                    Role = UserRoles.Employee
                });

                return password;
            }, ValidationContext.Valid);

            AddPersonalCommand.ToPropertyEx(this, vm => vm.Password);

            CancelCommand = ReactiveCommand.CreateFromTask(() => Shell.Current.GoToAsync("..", true));
        }
    }
}
