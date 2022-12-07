using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using ApoposTaskManager.Client.Services;
using ApoposTaskManager.Client.Views;
using ApropasTaskManager.Shared.ViewModels;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class ProjectInfoViewModel: ProjectViewModel
    {
        [Reactive] public UserViewModel Manager { get; set; }
        [Reactive] public bool IsManagerSelected { get; set; }
        public ReactiveCommand<Unit, Unit> AddUsers { get; set; }
        public ReactiveCommand<Unit, Unit> SelectManager { get; set; }
        public ProjectInfoViewModel(ProjectViewModel viewModel)
        {
            Id = viewModel.Id;
            Name = viewModel.Name;
            Description = viewModel.Description;
            Priority = viewModel.Priority;
            ProjectManagerId = viewModel.ProjectManagerId;
            Users = viewModel.Users;
            Missions = viewModel.Missions;


            AddUsers = ReactiveCommand.CreateFromTask(async () =>
            {
                var page = new AddEmployeeToProjectPage();
                page.ViewModel.Id = Id;

                await Shell.Current.Navigation.PushAsync(page);

                Users.Add(page.ViewModel.AddedEmployyes);
            });

            SelectManager = ReactiveCommand.CreateFromTask(async () =>
            {
                var page = new SelectManagerPage();
                page.ViewModel.Id = Id;

                await Shell.Current.Navigation.PushAsync(page);

                page.ViewModel.SelectManager.Subscribe(u => Manager = u);
            });

            this.WhenAnyValue(vm => vm.Manager).Subscribe(m => IsManagerSelected = m != null);
        }
    }
}
