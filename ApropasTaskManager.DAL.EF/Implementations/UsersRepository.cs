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
    private readonly UserManager<User> _users;

    public UsersRepository(UserManager<User> users)
    {
        _users = users;
    }

    public async Task<Result<Unit>> CreateAsync(User user)
    {
        if (await _users.FindByNameAsync(user.UserName) != null)
        {
            return Result<Unit>.CreateError(ServerDefaultResponses.UserExist);
        }

        var result  = await _users.CreateAsync(user);

        return ToResult(result, Unit.Default);
    }

    public async Task<Result<Unit>> CreateAsync(User user, string password)
    {
        if (await _users.FindByNameAsync(user.UserName) != null)
        {
            return Result<Unit>.CreateError(ServerDefaultResponses.UserExist);
        }

        var result = await _users.CreateAsync(user, password);

        return ToResult(result, Unit.Default);
    }

    public async Task<Result<Unit>> Delete(object id)
    {
        var user = await _users.FindByIdAsync((string)id);

        if (user == null)
        {
            return Result<Unit>.CreateError(ServerDefaultResponses.UserNotFound);
        }

        var result = await _users.DeleteAsync(user);

        return ToResult(result, Unit.Default);
    }


    public async Task<Result<IQueryable<User>>> FindAsync(Expression<Func<User, bool>> predicate)
    {
        return new( _users.Users.Where(predicate) );
    }

    public async Task<Result<IQueryable<User>>> GetAllAsync() => new(_users.Users);

    public async Task<Result<User>> GetAsyncById(object id) => await _users.FindByIdAsync((string)id);

    public async Task<Result<IQueryable<User>>> GetPageAsync(int pageIndex, int pageSize)
    {
        return new (_users.Users
                     .Skip(pageIndex * pageSize)
                     .Take(pageSize));
    }
    public async Task<Result<IQueryable<User>>> GetPageWhere(int pageIndex, int pageSize, Expression<Func<User, bool>> predicate)
    {
        return new( (await GetPageAsync(pageIndex, pageSize)).Value
                                                             .Where(predicate) );
    }
    public async Task<Result<Unit>> UpdateAsync(User user)
    {
        var result = await _users.UpdateAsync(user);

        return ToResult(result, Unit.Default);
    }

    private static Result<T> ToResult<T>(IdentityResult result, T value = default)
    {
        if (!result.Succeeded)
        {
            return Result<T>.CreateError(result.Errors);
        }

        return value;
    }
}
