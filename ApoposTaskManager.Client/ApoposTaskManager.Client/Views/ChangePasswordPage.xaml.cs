using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ApoposTaskManager.Client.ViewModels;
using ApropasTaskManager.Shared.ViewModels;
using ReactiveUI;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Formatters;
using ReactiveUI.Validation.Formatters.Abstractions;
using ReactiveUI.Validation.Helpers;
using ReactiveUI.Validation.ValidationBindings;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApoposTaskManager.Client.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChangePasswordPage : ReactiveContentPage<ChangePasswordViewModel>
	{
		public ChangePasswordPage ()
		{
			InitializeComponent ();

            ViewModel = new ChangePasswordViewModel();
        }

        protected override void OnAppearing()
        {
            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, vm => vm.OldPassword, v => v.oldPassword.Text)
                    .DisposeWith(disposables);

                this.Bind(ViewModel, vm => vm.NewPassword, v => v.newPassword.Text)
                    .DisposeWith(disposables);

                this.Bind(ViewModel, vm => vm.ConfirmPassword, v => v.confirmPassword.Text)
                    .DisposeWith(disposables);

                this.BindValidation(ViewModel, vm => vm.NewPassword, v => v.newPassword.ErrorMessage)
                    .DisposeWith(disposables);

                this.BindValidation(ViewModel, vm => vm.ConfirmPassword, v => v.confirmPassword.ErrorMessage)
                    .DisposeWith(disposables);
            });
        }
    }
}