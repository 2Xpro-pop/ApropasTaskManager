using System;
using System.Collections.Generic;
using System.ComponentModel;
using ApoposTaskManager.Client.Models;
using ApoposTaskManager.Client.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApoposTaskManager.Client.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item
        {
            get; set;
        }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}