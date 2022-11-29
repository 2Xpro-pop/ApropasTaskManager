using System;
using System.ComponentModel;
using ApoposTaskManager.Client.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApoposTaskManager.Client.Views
{
    public partial class AboutPage : ReactiveContentPage<AboutViewModel>
    {
        public AboutPage()
        {
            
            InitializeComponent();
        }
    }
}