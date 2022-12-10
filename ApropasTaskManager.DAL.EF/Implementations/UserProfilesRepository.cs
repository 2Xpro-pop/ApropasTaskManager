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

    public Task<Result<Unit>> CreateAsync(UserProfile user)
    {
        return this.CreateAsync(_db, _db.Profiles, user);
    }
    public Task<Result<Unit>> Delete(object id)
    {
        return this.DeleteAsync(_db, _db.Profiles, id, ServerDefaultResponses.UserNotFound);
    }
    public async Task<Result<IQueryable<UserProfile>>> FindAsync(Expression<Func<UserProfile, bool>> predicate)
    {
        return new(_db.Profiles.Where(predicate));
    }
    public async Task<Result<IQueryable<UserProfile>>> GetAllAsync() => new(_db.Profiles);

    public async Task<Result<UserProfile?>> GetAsyncById(object id)
    {
        return await _db.Profiles.FirstOrDefaultAsync(p => p.UserId.Equals(id));
    }
    public async Task<Result<IQueryable<UserProfile>>> GetPageAsync(int pageIndex, int pageSize)
    {
        return new(_db.Profiles.GetPage(pageIndex, pageSize));
    }
    public async Task<Result<IQueryable<UserProfile>>> GetPageWhere(int pageIndex, int pageSize, Expression<Func<UserProfile, bool>> predicate)
    {
        return new( _db.Profiles.GetPage(pageIndex, pageSize, predicate));
    }
    public Task<Result<Unit>> UpdateAsync(UserProfile userProfile)
    {
        return this.UpdateAsync(_db, _db.Profiles, userProfile, userProfile.UserId);
    }
}
