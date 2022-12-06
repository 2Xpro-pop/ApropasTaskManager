using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using ApoposTaskManager.Client.ViewModels;
using ApoposTaskManager.Client.Views;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace ApoposTaskManager.Client
{
    public partial class AppShell : ReactiveShell<ShellViewModel>
    {
        public AppShell()
        {
            InitializeComponent();

            ViewModel = new ShellViewModel();

            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(AddPersonalPage), typeof(AddPersonalPage));
            Routing.RegisterRoute(nameof(NewProjectPage), typeof(NewProjectPage));
            Routing.RegisterRoute(nameof(ChangePasswordPage), typeof(ChangePasswordPage));
            Routing.RegisterRoute(nameof(ChangeProfilePage), typeof(ChangeProfilePage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));

        }

        protected override void OnAppearing()
        {

        }
    }
}
