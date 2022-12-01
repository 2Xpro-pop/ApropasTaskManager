using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory = DependencyService.Get<IHttpClientFactory>();
        public async Task<bool> Login(string login, string password)
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

            await DependencyService.Get<IUserService>().GetUserInfo();

            return true;
        }
    }
}
