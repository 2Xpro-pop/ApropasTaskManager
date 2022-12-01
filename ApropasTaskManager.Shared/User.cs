using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ApropasTaskManager.Shared
{
    public class User: IdentityUser
    {
        public string Name { get; set; } 
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public UserRoles Role { get; set; }
    }
}
