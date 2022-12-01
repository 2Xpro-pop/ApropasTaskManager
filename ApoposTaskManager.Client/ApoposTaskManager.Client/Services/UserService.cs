using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ApoposTaskManager.Client.Services
{
    public class UserService : IUserService
    {
        public IObservable<User> User => _user;
        private Subject<User> _user = new Subject<User>();
        public async Task GetUserInfo()
        {
            var client = DependencyService.Get<IHttpClientFactory>().Create();

            var response = await client.GetAsync("api/user/");

            var json = await response.Content.ReadAsStringAsync();

            _user.OnNext(JsonConvert.DeserializeObject<User>(json));
        }

    }
}
