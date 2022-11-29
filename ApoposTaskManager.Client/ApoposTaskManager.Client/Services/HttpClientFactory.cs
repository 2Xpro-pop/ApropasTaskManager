using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ApoposTaskManager.Client.Services
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public string Jwt
        {
            get;
            set;
        }

        public HttpClient Create()
        {
            var client = new HttpClient();

            if (string.IsNullOrWhiteSpace(Jwt))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Jwt}");
            }

            client.BaseAddress = new Uri($"http://{App.BackendIp}:{App.BackendPort}");

            return client;
        }
    }
}
