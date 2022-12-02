using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.ViewModels
{
    public class PersonalInfoViewModel: ReactiveObject
    {
        [Reactive] public string RxText { get; set; }

        public PersonalInfoViewModel()
        {
            RxText = "Fody & RxUI ♡!";
        }
    }
}
