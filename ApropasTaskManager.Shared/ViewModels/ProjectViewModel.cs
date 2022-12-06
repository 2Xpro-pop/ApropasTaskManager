using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ApropasTaskManager.Shared.ViewModels
{
    [DataContract]
    public class ProjectViewModel : ReactiveObject
    {
        [Reactive, DataMember] public string Name { get; set; }
        [Reactive, DataMember] public string Description { get; set; }
        [Reactive, DataMember] public IEnumerable<int> Missions { get; set; }
        [Reactive, DataMember] public IEnumerable<string> Users { get; set; }

        public ProjectViewModel()
        {
        }

        public ProjectViewModel(Project project)
        {
            Name = project.Name;
            Description = project.Description;

            Missions = project.Missions.Select(m => m.Id);
            Users = project.Users.Select(p => p.Id);
        }

        /// <summary>
        /// Missions & Users will not apply
        /// </summary>
        /// <param name="project"></param>
        public void ApplyTo(Project project)
        {
            project.Name = Name;
            project.Description = Description;
        }
    }
}
