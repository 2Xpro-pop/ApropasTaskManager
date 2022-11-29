using System;
using System.Collections.Generic;
using ApoposTaskManager.Client.ViewModels;
using ApoposTaskManager.Client.Views;
using Xamarin.Forms;

namespace ApoposTaskManager.Client
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
