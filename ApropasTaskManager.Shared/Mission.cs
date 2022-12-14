using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApropasTaskManager.Shared
{
    /// <summary>
    /// The name "Task" is already taken,
    /// i don't want to write Shared.Task
    /// </summary>
    public class Mission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Deadline { get; set; }
        public MissionState Status { get; set; }
        
        public virtual List<User> Users { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
