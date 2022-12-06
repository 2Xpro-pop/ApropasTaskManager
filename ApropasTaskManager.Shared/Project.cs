using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApropasTaskManager.Shared
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        [ForeignKey("Users")]
        public string ProjectManagerId { get; set; }

        public virtual List<Mission> Missions { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
