using System;
using System.Collections.Generic;
using System.Text;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.BLL.DTO
{
    public class MissionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Deadline { get; set; }
        public MissionState Status { get; set; }
        
        public virtual List<UserDTO> Users { get; set; }
    }
}
