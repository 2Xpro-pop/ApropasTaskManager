using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ApoposTaskManager.Client.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApoposTaskManager.Client.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectsPage : ReactiveContentPage<ProjectsViewModel>
	{
		public ProjectsPage ()
		{
			InitializeComponent ();

            ViewModel = new ProjectsViewModel();
		}

        protected override void OnAppearing()
        {
            ViewModel.LoadingProjects.Execute();
            this.WhenActivated(disposables =>
            {

            });
        }
    }
}