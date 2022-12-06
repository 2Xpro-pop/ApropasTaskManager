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
        [Reactive, DataMember] public int Id { get; set; }
        [Reactive, DataMember] public string Name { get; set; }
        [Reactive, DataMember] public string Description { get; set; }
        [Reactive, DataMember] public int Priority { get; set; }
        [Reactive, DataMember] public string ProjectManagerId { get; set; }
        [Reactive, DataMember] public List<int> Missions { get; set; }
        [Reactive, DataMember] public List<string> Users { get; set; }

        public ProjectViewModel()
        {
        }

        public ProjectViewModel(Project project)
        {
            Id = project.Id;

            Name = project.Name;
            Description = project.Description;
            Priority = project.Priority;
            ProjectManagerId = project.ProjectManagerId;

            Missions = new List<int>(project.Missions.Select(m => m.Id));
            Users = new List<string>(project.Users.Select(p => p.Id));
        }

        /// <summary>
        /// Missions & Users will not apply
        /// </summary>
        /// <param name="project"></param>
        public void ApplyTo(Project project)
        {
            project.Name = Name;
            project.Description = Description;
            project.Priority = Priority;
            project.ProjectManagerId = ProjectManagerId;
        }
    }
}
