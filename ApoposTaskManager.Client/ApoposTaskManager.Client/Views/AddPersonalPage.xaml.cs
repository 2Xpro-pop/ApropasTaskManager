using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ApoposTaskManager.Client.ViewModels;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApoposTaskManager.Client.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddPersonalPage : ReactiveContentPage<AddPersonalViewModel>
	{
		public AddPersonalPage ()
		{
			InitializeComponent ();

            ViewModel = new AddPersonalViewModel();

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, vm => vm.Login, v => v.login.Text)
                    .DisposeWith(disposables);

                this.Bind(ViewModel, vm => vm.Name, v => v.name.Text)
                    .DisposeWith(disposables);

                this.Bind(ViewModel, vm => vm.Surname, v => v.surname.Text)
                    .DisposeWith(disposables);

                this.Bind(ViewModel, vm => vm.Middlename, v => v.middlename.Text)
                    .DisposeWith(disposables);

                this.BindValidation(ViewModel, vm => vm.Login, v => v.login.ErrorMessage)
                    .DisposeWith(disposables);

                this.BindValidation(ViewModel, vm => vm.Name, v => v.name.ErrorMessage)
                    .DisposeWith(disposables);

                this.BindValidation(ViewModel, vm => vm.Surname, v => v.surname.ErrorMessage)
                    .DisposeWith(disposables);

                this.BindValidation(ViewModel, vm => vm.Middlename, v => v.middlename.ErrorMessage)
                    .DisposeWith(disposables);

                ViewModel.AddPersonalCommand.ThrownExceptions.Subscribe(exc =>
                {
                    error.Text = "Error from server. Warum? Ich weiss nicht.";
                    error.IsVisible = true;
                }).DisposeWith(disposables);
            });
		}
    }
}