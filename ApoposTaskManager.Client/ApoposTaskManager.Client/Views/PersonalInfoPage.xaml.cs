using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApoposTaskManager.Client.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApoposTaskManager.Client.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonalInfoPage : ReactiveContentPage<PersonalInfoViewModel>
	{
		public PersonalInfoPage ()
		{
			InitializeComponent ();

            ViewModel = new PersonalInfoViewModel();
		}
	}
}