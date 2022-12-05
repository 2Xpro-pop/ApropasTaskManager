using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared;
using ApropasTaskManager.Shared.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory = DependencyService.Get<IHttpClientFactory>();

        public async Task<bool> ChangePassword(ResetPasswordViewModel resetPassword)
        {
            var client = _httpClientFactory.Create();

            var json = JsonConvert.SerializeObject(resetPassword);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/auth/reset-password", content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> LoginAsync(string login, string password)
        {
            var client = _httpClientFactory.Create();

            var queryBuilder = new QueryBuilder
                    {
                        { "login", login },
                        { "password", password }
                    };

            var response = await client.GetAsync("api/auth/login" + queryBuilder.ToString());

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            _httpClientFactory.Jwt = await response.Content.ReadAsStringAsync();

            await DependencyService.Get<IUserService>().GetCurrentUserInfoAsync();

            return true;
        }
    }
}
