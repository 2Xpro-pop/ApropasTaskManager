using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.DAL.Abstractions;
using ApropasTaskManager.Shared;
using ApropasTaskManager.Shared.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApropasTaskManager.DAL.EF.Implementations;
internal class UsersRepository: IUsersRepository
{
    private readonly ApplicationContext _db;
    private readonly UserManager<User> _users;

    public UsersRepository(ApplicationContext db, UserManager<User> users)
    {
        _db = db;
        _users = users;
    }

    public async Task<Result<Unit>> CreateAsync(User user)
    {
        var result  = await _users.CreateAsync(user);

        if (!result.Succeeded)
        {
            return Result<Unit>.CreateError(result.Errors);
        }

        return Unit.Default;
    }
    public async Task<Result<Unit>> Delete(object id)
    {
        var user = await _users.FindByIdAsync((string)id);

        if (user == null)
        {
            return Result<Unit>.CreateError(ServerDefaultResponses.UserNotFound);
        }

        var result = await _users.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return Result<Unit>.CreateError(result.Errors);
        }

        return Unit.Default;
    }


    public async Task<Result<IEnumerable<User>>> FindAsync(Expression<Func<User, bool>> predicate)
    {
        return new Result<IEnumerable<User>>(
            _users.Users.Where(predicate)
        );
    }

    public async Task<Result<IEnumerable<User>>> GetAllAsync() => await _users.Users.ToArrayAsync();

    public async Task<Result<User>> GetAsyncById(object id) => await _users.FindByIdAsync((string)id);

    public async Task<Result<IEnumerable<User>>> GetPageAsync(int pageIndex, int pageSize)
    {
        return _users.Users
                     .Skip(pageIndex * pageSize)
                     .Take(pageSize);
    }
    public async Task<Result<IEnumerable<User>>> GetPageWhere(int pageIndex, int pageSize, Expression<Func<User, bool>> predicate)
    {
        return (await GetPageAsync(pageIndex, pageSize)).Value
                                                        .Where(predicate);
    }
    public async Task<Result<Unit>> UpdateAsync(User user)
    {
        var result = await _users.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return Result<Unit>.CreateError(result.Errors);
        }

        return  
    }
}
