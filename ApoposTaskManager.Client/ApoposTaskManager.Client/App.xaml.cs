﻿using System;
using ApoposTaskManager.Client.Services;
using ApoposTaskManager.Client.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApoposTaskManager.Client
{
    public partial class App : Application
    {
        public const string BackendIp = "localhost";
        public const string BackendPort = "5186";

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<IAuthService, AuthService>();
            DependencyService.RegisterSingleton<IHttpClientFactory>(new HttpClientFactory());
            DependencyService.RegisterSingleton<IUserService>(new UserService());

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
