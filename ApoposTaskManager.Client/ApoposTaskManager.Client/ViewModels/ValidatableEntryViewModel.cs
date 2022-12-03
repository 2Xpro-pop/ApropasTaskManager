using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class ValidatableEntryViewModel: ReactiveObject
    {
        [Reactive] public string Text { get; set; }
        [Reactive] public string ErrorMessage { get; set; }
        [Reactive] public string Placeholder { get; set; }
        [Reactive] public bool IsPassword { get; set; }

        public ValidatableEntryViewModel()
        {

        }
    }
}
