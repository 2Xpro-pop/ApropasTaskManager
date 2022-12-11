using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ApoposTaskManager.Client.Services;
using ApoposTaskManager.Client.Views;
using ApropasTaskManager.BLL.DTO;
using ApropasTaskManager.Shared;
using ApropasTaskManager.Shared.ViewModels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class NewProjectViewModel: CreateProjectViewModel
    {
        [Reactive] public bool IsManagerSelected { get; set; }
        [Reactive] public UserDTO Manager { get; set; }
        [Reactive] public string Error { get; set; }
        public ReactiveCommand<Unit, string> Save { get; }
        public ReactiveCommand<Unit, Unit> SelectManager { get; set; }

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
