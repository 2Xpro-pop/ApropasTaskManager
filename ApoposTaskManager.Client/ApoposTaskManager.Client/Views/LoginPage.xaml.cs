using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ApoposTaskManager.Client.ViewModels;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApoposTaskManager.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ReactiveContentPage<LoginViewModel>
    {
        public LoginPage()
        {
            InitializeComponent();

            ViewModel = new LoginViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // For visual effects you need to use view
            // for validation viewModel
            this.WhenActivated(disposables =>
            {
                this.WhenAnyValue(v => v.ViewModel.Login)
                    .Skip(1) // Skip first change(it's just start value)
                    .Subscribe(login =>
                    {
                        if (string.IsNullOrEmpty(login))
                        {
                            loginWarning.IsVisible = true;
                            loginWarning.Text = "The login cannot be empty!";
                        }
                        else
                        {
                            loginWarning.IsVisible = false;
                        }
                    }).DisposeWith(disposables);

                this.WhenAnyValue(v => v.ViewModel.Password)
                    .Skip(1) // Skip first change(it's just start value)
                    .Subscribe(password =>
                    {
                        if (string.IsNullOrEmpty(password) || password.Length <= 8)
                        {
                            passwordWarning.IsVisible = true;
                            passwordWarning.Text = "The password must be greater than 8";
                        }
                        else
                        {
                            passwordWarning.IsVisible = false;
                        }
                    }).DisposeWith(disposables);

                ViewModel.LoginCommand.Subscribe(result =>
                {
                    if (!result)
                    {
                        error.IsVisible = true;
                        error.Text = "Invalid username or password";
                    }
                }).DisposeWith(disposables);

                ViewModel.LoginCommand.ThrownExceptions.Subscribe(exc =>
                {
                    error.IsVisible = true;
                    error.Text = "Failed to establish a connection with the server";
                }).DisposeWith(disposables);
            });
        }
    }
}