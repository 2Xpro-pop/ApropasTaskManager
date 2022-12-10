using System;
using System.Collections.Generic;
using System.Text;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public UserRoles Role { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }
    }
}
