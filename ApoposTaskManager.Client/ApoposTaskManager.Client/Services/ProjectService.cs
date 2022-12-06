using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared.ViewModels;
using Xamarin.Forms;

namespace ApoposTaskManager.Client.Services
{
    public class ProjectService : IProjectService
    {
        public async Task<IEnumerable<ProjectViewModel>> GetProjects()
        {
            var client = DependencyService.Get<IHttpClientFactory>().Create();

            return await client.GetJsonAsync<List<ProjectViewModel>>("api/projects");
        }
    }
}
