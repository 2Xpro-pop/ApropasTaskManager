using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using ApoposTaskManager.Client.Models;
using ApoposTaskManager.Client.Services;
using ApoposTaskManager.Client.Views;
using ApropasTaskManager.BLL.DTO;
using ApropasTaskManager.Shared;
using ApropasTaskManager.Shared.ViewModels;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class ProjectsViewModel: ReactiveObject
    {
        [Reactive] public bool IsErrorVisible { get; set; }
        [Reactive] public string ErrorMessage { get; set; }
        [Reactive] public bool IsDirector { get; set; }
        [Reactive] public bool IsBusy { get; set; }

        public ObservableCollection<ProjectDTO> Projects { get; } 
        public ReactiveCommand<Unit, Unit> LoadingProjects { get; }
        public ReactiveCommand<Unit, Unit> AddProject { get; }
        public ReactiveCommand<ProjectDTO, Unit> OpenProject { get; }
        public ProjectsViewModel()
        {
            Projects = new ObservableCollection<ProjectDTO>();

            LoadingProjects = ReactiveCommand.CreateFromTask(async () =>
            {
                var projects = await DependencyService.Get<IClientProjectService>().GetProjects();

                Projects.Clear();

                foreach (var project in projects)
                {
                    Projects.Add(project);
                }

            });

            LoadingProjects.CanExecute.Subscribe(can => IsBusy = !can);

            LoadingProjects.ThrownExceptions.Subscribe(exc =>
            {
                ErrorMessage = ServerDefaultResponses.NetExceptions;
                IsBusy = false;
            });

            AddProject = ReactiveCommand.CreateFromTask(
                () => Shell.Current.GoToAsync(nameof(NewProjectPage)),
                this.WhenAnyValue(vm => vm.IsBusy).Select(b => !b))
            ;

            OpenProject = ReactiveCommand.CreateFromTask(async (ProjectDTO project) =>
            {
                var projectPage = new ProjectInfoPage
                {
                    ViewModel = new ProjectInfoViewModel(project)
                };

                await Shell.Current.Navigation.PushAsync(projectPage);
            });

            this.WhenAnyValue(vm => vm.ErrorMessage).Subscribe(msg=> IsErrorVisible = !string.IsNullOrEmpty(msg));

            DependencyService.Get<IUserService>().User.Subscribe(user => IsDirector = user.Role == UserRoles.Director);
        }
    }
}
