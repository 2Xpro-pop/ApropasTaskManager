using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApoposTaskManager.Client.ViewModels;
using ApropasTaskManager.BLL.DTO;
using ApropasTaskManager.Shared.ViewModels;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.Services
{
    public class ClientProjectService : IClientProjectService
    {
        public async Task<IEnumerable<ProjectDTO>> GetProjects()
        {
            var client = DependencyService.Get<IHttpClientFactory>().Create();
            var result = await client.GetJsonAsync<List<ProjectDTO>>("api/project");
            return result;
        }

        public async Task<string> CreateProject(CreateProjectViewModel projectViewModel)
        {
            var client = DependencyService.Get<IHttpClientFactory>().Create();

            var response = await client.PostAsJsonAsync("api/project", projectViewModel);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PutUser(int projectId, string userId)
        {
            var client = DependencyService.Get<IHttpClientFactory>().Create();

            // NULL, это просто заглушка!
            var response = await client.PutAsJsonAsync($"api/project/{projectId}/{userId}", 0);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SelectManager(int projectId, string userId)
        {
            var client = DependencyService.Get<IHttpClientFactory>().Create();

            // NULL, это просто заглушка!
            var response = await client.PutAsJsonAsync($"api/project/select-manager/{projectId}/{userId}", 0);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
