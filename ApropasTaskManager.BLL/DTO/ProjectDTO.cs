using System;
using System.Collections.Generic;
using System.Text;

namespace ApropasTaskManager.BLL.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string ProjectManagerId { get; set; }
        public List<MissionDTO> Missions { get; set; }
        public List<ProjectDTO> Users { get; set; }
    }
}
