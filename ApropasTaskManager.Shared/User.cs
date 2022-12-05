using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ApropasTaskManager.Shared
{
    public class User: IdentityUser
    {
        public UserRoles Role { get; set; }

        public virtual UserProfile Profile { get; set; }
        public virtual List<Mission> Missions { get; set; }
        public virtual List<Project> Projects { get; set; }
    }
}
