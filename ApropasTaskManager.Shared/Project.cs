using System;
using System.Collections.Generic;
using System.Text;

namespace ApropasTaskManager.Shared
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Mission> Missions { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
