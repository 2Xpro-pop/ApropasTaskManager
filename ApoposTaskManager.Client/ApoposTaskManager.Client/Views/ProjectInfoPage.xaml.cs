using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApoposTaskManager.Client.Services;
using ApoposTaskManager.Client.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApoposTaskManager.Client.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectInfoPage : ReactiveContentPage<ProjectInfoViewModel>
	{
		public ProjectInfoPage()
		{
			InitializeComponent();
		}

        protected async override void OnAppearing()
        {
            ViewModel.Manager = await DependencyService.Get<IUserService>().GetUserByIdAsync(ViewModel.ProjectManagerId);
        }
    }
}