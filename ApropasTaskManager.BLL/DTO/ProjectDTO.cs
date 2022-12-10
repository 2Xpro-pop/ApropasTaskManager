using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.BLL.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string ProjectManagerId { get; set; }
        public IEnumerable<MissionDTO> Missions { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }

        public ProjectDTO() { }
        public ProjectDTO(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            Description = project.Description;
            Priority = project.Priority;
            ProjectManagerId = project.ProjectManagerId;

            Missions = project.Missions.Select(m => new MissionDTO(m));
            Users = project.Users.Select(u => new UserDTO(u));
        }

        public Project ToProject()
        {
            return new Project()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Priority = Priority,
                ProjectManagerId = ProjectManagerId,
                Missions = new List<Mission>(Missions.Select( m => m.ToMission()))
            };
        }
    }
}
