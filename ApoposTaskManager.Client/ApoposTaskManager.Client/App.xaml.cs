using System;
using ApoposTaskManager.Client.Services;
using ApoposTaskManager.Client.Views;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApoposTaskManager.Client
{
    public partial class App : Application
    {
        public const string BackendIp = "localhost";
        public const string BackendPort = "5186";

        static JsonSerializerSettings SerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<IAuthService, AuthService>();
            DependencyService.Register<IProjectService, ProjectService>();
            DependencyService.RegisterSingleton<IHttpClientFactory>(new HttpClientFactory());
            DependencyService.RegisterSingleton<IUserService>(new UserService());

            JsonConvert.DefaultSettings = SerializerSettings;

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
