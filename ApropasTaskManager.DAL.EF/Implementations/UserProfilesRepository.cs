using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.DAL.Abstractions;
using ApropasTaskManager.Shared;
using Microsoft.EntityFrameworkCore;

namespace ApropasTaskManager.DAL.EF.Implementations;
internal class UserProfilesRepository : IUserProfilesRepository
{
    private readonly ApplicationContext _db;

    public UserProfilesRepository(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<Result<Unit>> CreateAsync(UserProfile user)
    {
        await _db.AddAsync(user);
        await _db.SaveChangesAsync();

        return Unit.Default;
    }
    public async Task<Result<Unit>> Delete(object id)
    {
        var profile = (await GetAsyncById(id)).Value;

        if (profile == null)
        {
            return Result<Unit>.CreateError(ServerDefaultResponses.UserNotFound);
        }

        _db.Profiles.Remove(profile);

        await _db.SaveChangesAsync();
        
        return Unit.Default;
    }


    public async Task<Result<IQueryable<UserProfile>>> FindAsync(Expression<Func<UserProfile, bool>> predicate)
    {
        return new(_db.Profiles.Where(predicate));
    }

    public async Task<Result<IQueryable<UserProfile>>> GetAllAsync() => new(_db.Profiles);

    public async Task<Result<UserProfile?>> GetAsyncById(object id) => await _db.Profiles.FirstOrDefaultAsync(p => p.UserId.Equals(id));

    public async Task<Result<IQueryable<UserProfile>>> GetPageAsync(int pageIndex, int pageSize)
    {
        return new(_db.Profiles
                      .Skip(pageIndex * pageSize)
                      .Take(pageSize));
    }
    public async Task<Result<IQueryable<UserProfile>>> GetPageWhere(int pageIndex, int pageSize, Expression<Func<UserProfile, bool>> predicate)
    {
        return new((await GetPageAsync(pageIndex, pageSize)).Value
                                                            .Where(predicate));
    }
    public async Task<Result<Unit>> UpdateAsync(UserProfile userProfile)
    {
        var profile = await GetAsyncById(userProfile.UserId);

        if (!profile)
        {
            return Result<Unit>.CreateError(profile.Error);
        }

        return Unit.Default;
    }
}
