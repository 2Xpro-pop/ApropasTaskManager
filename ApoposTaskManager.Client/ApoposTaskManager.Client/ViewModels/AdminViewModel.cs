using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ApoposTaskManager.Client.Views;
using ReactiveUI;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class AdminViewModel: ReactiveObject
    {
        public ICommand AddPersonalCommand
        {
            get;
        }

        public AdminViewModel()
        {
            AddPersonalCommand = new Command(async () =>
            {
                try
                {

                    await Shell.Current.GoToAsync(nameof(AddPersonalPage));
                }
                catch (Exception exc)
                {
                    
                }
            });
        }
        
    }
}
