﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared.ViewModels;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.Services
{
    public class ClientProjectService : IClientProjectService
    {
        public async Task<IEnumerable<ProjectViewModel>> GetProjects()
        {
            var client = DependencyService.Get<IHttpClientFactory>().Create();
            var result = await client.GetJsonAsync<List<ProjectViewModel>>("api/project");
            return result;
        }

        public async Task<string> CreateProject(ProjectViewModel projectViewModel)
        {
            var client = DependencyService.Get<IHttpClientFactory>().Create();

            var response = await client.PostAsJsonAsync("api/project", projectViewModel);

            return await response.Content.ReadAsStringAsync();
        }
    }
}