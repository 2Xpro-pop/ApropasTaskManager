using System;
using System.Reactive.Linq;
using System.Windows.Input;
using ApoposTaskManager.Client.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class AboutViewModel : ReactiveObject
    {
        [Reactive]
        public int Counter { get; set; }
        public ICommand IncrementCommand { get; }

        [Reactive]
        public string FirstName
        {
            get;
            set;
        }

        [Reactive]
        public string LastName
        {
            get;
            set;
        }

        [ObservableAsProperty]
        public string FullName
        {
            get;
        }

        public AboutViewModel()
        {
            IncrementCommand = ReactiveCommand.Create(() => Counter++);

            FirstName = DependencyService.Get<IHttpClientFactory>().Jwt;

            this.WhenAnyValue(vm => vm.FirstName, vm => vm.LastName)
                .Select(t => $"{t.Item1} {t.Item2}")
                .ToPropertyEx(this, vm  => vm.FullName);
        }
    }
}