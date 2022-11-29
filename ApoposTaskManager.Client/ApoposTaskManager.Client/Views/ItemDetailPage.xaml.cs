using System.ComponentModel;
using ApoposTaskManager.Client.ViewModels;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}