/*#define DIRECTOR*/

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
using ReactiveUI.Validation.Extensions;
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
                this.Bind(ViewModel, vm => vm.Login, v => v.loginEntry.Text)
                    .DisposeWith(disposables);

                this.Bind(ViewModel, vm => vm.Password, v => v.passwordEntry.Text)
                    .DisposeWith(disposables);

                this.BindValidation(ViewModel, vm => vm.Login, v => v.loginEntry.ErrorMessage)
                    .DisposeWith(disposables);

                this.BindValidation(ViewModel, vm => vm.Password, v => v.passwordEntry.ErrorMessage)
                    .DisposeWith(disposables);

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
#if DEBUG

#if DIRECTOR
            ViewModel.Login = "director";
            ViewModel.Password = "Apr@12345";
            ViewModel.LoginCommand.Execute();
#endif

#endif
        }
    }
}