using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ApoposTaskManager.Client.Views;
using ApropasTaskManager.Shared.ViewModels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class ProjectInfoViewModel: ProjectViewModel
    {
        public ReactiveCommand<Unit, Unit> AddUsers { get; set; }
        public ProjectInfoViewModel(ProjectViewModel viewModel)
        {
            Id = viewModel.Id;
            Name = viewModel.Name;
            Description = viewModel.Description;
            Users = viewModel.Users;
            Missions = viewModel.Missions;


            AddUsers = ReactiveCommand.CreateFromTask(async () =>
            {
                var page = new AddEmployeeToProjectPage();
                page.ViewModel.Id = Id;

                await Shell.Current.Navigation.PushAsync(page);
            });
        }
    }
}
