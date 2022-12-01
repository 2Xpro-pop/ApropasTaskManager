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
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class AddPersonalViewModel: ReactiveObject
    {
        [Reactive] public string Login { get; set; }
        [Reactive] public string Name { get; set; }
        [Reactive] public string Surname { get; set; }
        [Reactive] public string Middlename { get; set; }
        [ObservableAsProperty] public string Password { get; }
        public ReactiveCommand<Unit, string> AddPersonalCommand { get; } 
        public ICommand CancelCommand { get; } 

        public AddPersonalViewModel()
        {
            var validation = this.WhenAnyValue(vm => vm.Login, vm => vm.Name, vm => vm.Surname, vm => vm.Middlename, (login, name, surname, middlename) =>
            {
                // ÍÅ ÑÄÅËÀË Â ÎÄÍÓ ÑÒÐÎÊÓ ÑÏÅÖÈÀËÜÍÎ
                if (string.IsNullOrEmpty(login) || login.Contains(" "))
                    return false;
                if (string.IsNullOrEmpty(name))
                    return false;
                if (string.IsNullOrEmpty(surname))
                    return false;
                if (string.IsNullOrEmpty(middlename))
                    return false;
                return true;
            });

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
            }, validation);

            AddPersonalCommand.ToPropertyEx(this, vm => vm.Password);

            CancelCommand = ReactiveCommand.CreateFromTask(() => Shell.Current.GoToAsync("..", true));
        }
    }
}
