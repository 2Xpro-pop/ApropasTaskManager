using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using ApoposTaskManager.Client.Services;
using ApoposTaskManager.Client.Views;
using ApropasTaskManager.BLL.DTO;
using ApropasTaskManager.Shared.ViewModels;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class ProjectInfoViewModel
    {
        public int Id { get; }
        [Reactive] public string Name { get; set; }
        [Reactive] public string Description { get; set; }
        [Reactive] public int Priority { get; set; }
        [Reactive] public string ProjectManagerId { get; set; }
        public ObservableCollection<UserDTO> Users { get; }
        public ObservableCollection<MissionDTO> Missions { get; }
        [Reactive] public UserDTO Manager { get; set; }
        [Reactive] public bool IsManagerSelected { get; set; }
        public ReactiveCommand<Unit, Unit> AddUsers { get; set; }
        public ReactiveCommand<Unit, Unit> SelectManager { get; set; }
        public ProjectInfoViewModel(ProjectDTO viewModel)
        {
            Id = viewModel.Id;
            Name = viewModel.Name;
            Description = viewModel.Description;
            Priority = viewModel.Priority;
            ProjectManagerId = viewModel.ProjectManagerId;
            Manager = viewModel.ProjectManager;
            Users = new ObservableCollection<UserDTO>(viewModel.Users);
            Missions = new ObservableCollection<MissionDTO>(viewModel.Missions);


            AddUsers = ReactiveCommand.CreateFromTask(async () =>
            {
                var page = new AddEmployeeToProjectPage();
                page.ViewModel.Id = Id;

                await Shell.Current.Navigation.PushAsync(page);
            });

            SelectManager = ReactiveCommand.CreateFromTask(async () =>
            {
                var page = new SelectManagerPage();
                page.ViewModel.Id = Id;

                await Shell.Current.Navigation.PushAsync(page);

                /*page.ViewModel.SelectManager.Subscribe(u => Manager = u);*/
            });

            this.WhenAnyValue(vm => vm.Manager).Subscribe(m => IsManagerSelected = m != null);
        }
    }
}
