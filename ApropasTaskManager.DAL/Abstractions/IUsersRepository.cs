using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.DAL.Abstractions
{
    public interface IUsersRepository: IPaginationableRepository<User>
    {
        Task<Result<Unit>> CreateAsync(User user, string password);
    }
}
