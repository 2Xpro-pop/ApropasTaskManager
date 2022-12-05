using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApoposTaskManager.Client.Services;
using ApropasTaskManager.Shared;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ApoposTaskManager.Client
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient httpClient, string url, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, url)
            {
                Content = content
            };

            return httpClient.SendAsync(request);

        }
    }
}
