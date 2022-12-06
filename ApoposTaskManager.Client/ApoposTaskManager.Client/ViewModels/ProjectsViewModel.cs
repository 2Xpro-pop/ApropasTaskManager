using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;
using ApoposTaskManager.Client.Services;
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
        [Reactive] public bool IsBusy { get; set; }

        public ObservableCollection<ProjectViewModel> Projects { get; } = new ObservableCollection<ProjectViewModel>();
        public ReactiveCommand<Unit, Unit> LoadingProjects { get; }

        public ProjectsViewModel()
        {
            LoadingProjects = ReactiveCommand.CreateFromTask(async () =>
            {
                var projects = await DependencyService.Get<IProjectService>().GetProjects();

                Projects.Clear();
                Projects.Add(projects);
            });

            LoadingProjects.CanExecute.Subscribe(can => IsBusy = !can);
        }
    }
}
