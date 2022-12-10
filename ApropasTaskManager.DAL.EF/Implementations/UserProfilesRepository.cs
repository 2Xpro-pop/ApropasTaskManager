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
internal class UserProfilesRepository : IUserProfilesRepository, IRepositoryWithDbContext<UserProfile>
{
    private readonly ApplicationContext _db;

    public UserProfilesRepository(ApplicationContext db)
    {
        _db = db;
    }

    public virtual DbContext Context => _db;
    public virtual DbSet<UserProfile> Values => _db.Profiles;

    public Task<Result<Unit>> CreateAsync(UserProfile user)
    {
        return this.CreateAsyncWithContext(user);
    }
    public Task<Result<Unit>> Delete(object id)
    {
        return this.DeleteAsyncWithContext(id, ServerDefaultResponses.UserNotFound);
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
        return this.UpdateAsyncWithContext(userProfile, userProfile.UserId);
    }
}
