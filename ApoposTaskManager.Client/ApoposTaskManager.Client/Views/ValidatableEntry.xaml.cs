using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ApoposTaskManager.Client.ViewModels;
using ReactiveUI;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApoposTaskManager.Client.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ValidatableEntry : ReactiveContentView<ValidatableEntryViewModel>
	{
        public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create(
            nameof(ErrorMessage),
            typeof(string),
            typeof(ValidatableEntry),
            defaultBindingMode: BindingMode.TwoWay
        );

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(ValidatableEntry),
            defaultBindingMode: BindingMode.TwoWay
        );

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(ValidatableEntry)
        );

        public string ErrorMessage
        {
            get => (string)GetValue(ErrorMessageProperty);
            set => SetValue(ErrorMessageProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public ValidatableEntry ()
		{
			InitializeComponent();

            ViewModel = new ValidatableEntryViewModel();

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, vm => vm.Text, v => v.Text)
                    .DisposeWith(disposables);

                this.Bind(ViewModel, vm => vm.ErrorMessage, v => v.ErrorMessage)
                    .DisposeWith(disposables);

                this.Bind(ViewModel, vm => vm.Placeholder, v => v.Placeholder)
                    .DisposeWith(disposables);
            });
            
		}
	}
}