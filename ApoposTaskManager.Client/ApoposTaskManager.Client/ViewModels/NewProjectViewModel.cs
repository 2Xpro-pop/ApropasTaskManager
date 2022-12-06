using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ApoposTaskManager.Client.Services;
using ApropasTaskManager.Shared;
using ApropasTaskManager.Shared.ViewModels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class NewProjectViewModel: ProjectViewModel
    {
        [Reactive] public string Error { get; set; }
        public ReactiveCommand<Unit, string> Save { get; }

        public NewProjectViewModel()
        {
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                return await DependencyService.Get<IClientProjectService>().CreateProject(this);
            });

            Save.Subscribe(response =>
            {
                if (!int.TryParse(response, out var id))
                {
                    Error = response;
                }
            });

            Save.ThrownExceptions.Subscribe(exc =>
            {
                Error = ServerDefaultResponses.NetExceptions;
            });
        }
    }
}
