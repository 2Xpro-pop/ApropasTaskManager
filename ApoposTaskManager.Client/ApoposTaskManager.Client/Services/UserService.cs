using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared;
using ApropasTaskManager.Shared.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ApoposTaskManager.Client.Services
{
    public class UserService : IUserService
    {
        public IObservable<UserViewModel> User => _user;
        private BehaviorSubject<UserViewModel> _user = new BehaviorSubject<UserViewModel>(null);
        public async Task GetCurrentUserInfoAsync()
        {
            var client = DependencyService.Get<IHttpClientFactory>().Create();

            var response = await client.GetAsync("api/user/");

            var json = await response.Content.ReadAsStringAsync();

            _user.OnNext(JsonConvert.DeserializeObject<UserViewModel>(json));
        }

        public async Task<string> CreateUserAsync(UserViewModel user)
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

        public async Task<bool> UpdateUserAsync(JsonPatchDocument<UserViewModel> jsonPatch)
        {
            var client = DependencyService.Get<IHttpClientFactory>().Create();

            var response = await client.PatchAsJsonAsync("api/user/", jsonPatch);

            if (!response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException("Bad request maybe");
            }

            jsonPatch.ApplyTo(_user.Value);
            _user.OnNext(_user.Value);

            return true;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsersPage(int page, int pageSize)
        {
            var client = DependencyService.Get<IHttpClientFactory>().Create();

            var response = await client.GetJsonAsync<List<UserViewModel>>($"api/user/{page}/{pageSize}");

            return response;
        }

        public async Task<IEnumerable<UserViewModel>> GetManagersPage(int page, int pageSize)
        {
            var client = DependencyService.Get<IHttpClientFactory>().Create();

            var response = await client.GetJsonAsync<List<UserViewModel>>($"api/user/managers/{page}/{pageSize}");

            return response;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsersByName(string name)
        {
            var client = DependencyService.Get<IHttpClientFactory>().Create();

            var queryBuilder = new QueryBuilder
                    {
                        { "name", name },
                    };

            var response = await client.GetJsonAsync<List<UserViewModel>>($"api/user/find-by-name" +queryBuilder.ToString());

            return response;
        }
    }
}
