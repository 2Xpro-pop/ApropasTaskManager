using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.BLL.DTO;
using ApropasTaskManager.DAL.Abstractions;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.BLL.Services
{
    public class UserService
    {
        private readonly IUsersRepository _users;

        public UserService(IUsersRepository users)
        {
            _users = users;
        }

        public async Task<Result<Unit>> CreateUser(UserDTO user, string password)
        {
            return await _users.CreateAsync(user.ToUser(), password);
        }
    }
}
