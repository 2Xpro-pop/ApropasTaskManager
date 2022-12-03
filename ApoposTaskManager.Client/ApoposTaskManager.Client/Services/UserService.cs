using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ApoposTaskManager.Client.Services
{
    public class UserService : IUserService
    {
        public IObservable<User> User => _user;
        private BehaviorSubject<User> _user = new BehaviorSubject<User>(null);
        public async Task GetCurrentUserInfoAsync()
        {
            var client = DependencyService.Get<IHttpClientFactory>().Create();

            var response = await client.GetAsync("api/user/");

            var json = await response.Content.ReadAsStringAsync();

            _user.OnNext(JsonConvert.DeserializeObject<User>(json));
        }

        public async Task<string> CreateUserAsync(User user)
        {
            var client = DependencyService.Get<IHttpClientFactory>().Create();

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/auth/register", content);

            if (!response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException("Bad request maybe");
            }

            var password = await response.Content.ReadAsStringAsync();

            return password;
        }
    }
}
