using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using ApoposTaskManager.Client.Models;
using ApoposTaskManager.Client.Services;
using ApropasTaskManager.Shared;
using ApropasTaskManager.Shared.ViewModels;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class AddEmployeeToProjectViewModel: ReactiveObject
    {
        public int Id { get; set; }
        [Reactive] public string SearchText { get; set; }
        [Reactive] public bool IsBusy { get; set; }
        [Reactive] public bool IsNeedRefresh { get; set; }
        [Reactive] public int ItemTreshold { get; set; }
        [Reactive] public int PageIndex { get; set; }
        [Reactive] public ObservableCollection<UserViewModel> Users { get; set; }
        public ReactiveCommand<Unit,Unit> Search { get; set; }
        public ReactiveCommand<Unit,Unit> ItemTresholdReached { get;  }
        public ReactiveCommand<Unit,Unit> Refresh { get;  }
        public ReactiveCommand<UserViewModel,Unit> AddEmployeeToProject { get;  }
        public List<string> AddedEmployyes { get; set;}

        public AddEmployeeToProjectViewModel()
        {
            AddedEmployyes = new List<string>();
            Users = new ObservableCollection<UserViewModel>();

            ItemTreshold = 1;
            ItemTresholdReached = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;

                PageIndex++;
                var items = await DependencyService.Get<IUserService>().GetUsersPageAsyn(PageIndex, 10);

                if (items.Count() == 0)
                {
                    ItemTreshold = -1;
                    return;
                }

                foreach (var item in items)
                {
                    if (item.Projects.Contains(Id))
                    {
                        continue;
                    }
                    Users.Add(item);
                }

            }, this.WhenAnyValue(vm => vm.IsBusy).Select(b => !b));

            Refresh = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                ItemTreshold = 1;
                PageIndex = 0;
                Users.Clear();
                await ItemTresholdReached.Execute();
            });

            Refresh.CanExecute.Subscribe(b => IsBusy = !b);

            Search = ReactiveCommand.CreateFromTask(async () =>
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    await Refresh.Execute();
                    return;
                }
                ItemTreshold = -1;

                var items = await DependencyService.Get<IUserService>().GetUsersByNameAsync(SearchText);

                Users.Clear();
                Users.Add(items.Where(u => !u.Projects.Contains(Id)));
            });

            AddEmployeeToProject = ReactiveCommand.CreateFromTask(async (UserViewModel user) =>
            {
                var response = await DependencyService.Get<IClientProjectService>().PutUser(Id, user.Id);

                if (int.TryParse(response, out var result))
                {
                    AddedEmployyes.Add(user.Id);
                    Users.Remove(user);
                }
            });
        }
    }
}
