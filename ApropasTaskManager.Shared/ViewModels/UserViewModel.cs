using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApropasTaskManager.Shared.ViewModels
{
    public class UserViewModel
    {
        public string Id
        {
            get; set;
        }
        public string UserName
        {
            get; set;
        }
        public string Email
        {
            get; set;
        }
        public UserRoles Role
        {
            get; set;
        }
        public UserProfile Profile
        {
            get; set;
        }
        public List<int> Projects
        {
            get; set;
        }

        public UserViewModel()
        {
        }

        public UserViewModel(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            Profile = user.Profile;
            Role = user.Role;
            Projects = new List<int>(user.Projects.Select(p => p.Id));
        }

        public void ApplyToUser(User user)
        {
            user.UserName = UserName;
            user.Email = Email;
        }
    }

}
