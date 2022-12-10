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
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }
        public string Email { get; set; }

        public UserDTO() { }
        public UserDTO(User user)
        {
            Id = user.Id;
            Role = user.Role;
            Login = user.UserName;
            Email = user.Email;

            Name = user.Profile.Name;
            Surname = user.Profile.Surname;
            Middlename = user.Profile.MiddleName;
        }

        public User ToUser()
        {
            return new User()
            {
                Id = Id,
                Role = Role,
                UserName = Login, 
                Email = Email,
                Profile = new UserProfile()
                {
                    Name = Name,
                    Surname = Surname,
                    MiddleName = Middlename
                }
            };
        }
    }
}
