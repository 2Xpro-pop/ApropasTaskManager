using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public IEnumerable<UserDTO> Users { get; set; }

        public MissionDTO() { }
        public MissionDTO(Mission mission)
        {
            Id = mission.Id;
            Name = mission.Name;
            Description = mission.Description;
            CreatedAt = mission.CreatedAt;
            Deadline = mission.Deadline;
            Status = mission.Status;

            Users = mission.Users.Select(u => new UserDTO(u));
        }

        public Mission ToMission()
        {
            return new Mission()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                CreatedAt = CreatedAt,
                Deadline = Deadline,
                Status = Status,
                Users = new List<User>(Users.Select(u => u.ToUser())),
            };
        }
    }
}
