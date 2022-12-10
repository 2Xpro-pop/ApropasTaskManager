using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.DAL.EF.Implementations;
using ApropasTaskManager.Shared;
using Microsoft.EntityFrameworkCore;

namespace ApropasTaskManager.DAL.EF;
internal abstract class BaseRepositoryWithDbContext<T> : IRepositoryWithDbContext<T> where T: class
{
    protected readonly ApplicationContext _db;

    public BaseRepositoryWithDbContext(ApplicationContext db)
    {
        _db = db;
    }

    public virtual DbContext Context => _db;
    public virtual DbSet<T> Values { get; protected set; }
    protected object DeleteError { get; set; }
    protected Expression<Func<T, bool>> IdEqualsPredicate { get; set; }
    protected Func<T, object> IdGetter { get; set; }

    public Task<Result<Unit>> CreateAsync(T user)
    {
        return this.CreateAsyncWithContext(user);
    }
    public Task<Result<Unit>> Delete(object id)
    {
        return this.DeleteAsyncWithContext(id, DeleteError);
    }
    public async Task<Result<IQueryable<T>>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return new(Values.Where(predicate));
    }
    public async Task<Result<IQueryable<T>>> GetAllAsync() => new(Values);

    public async Task<Result<T?>> GetAsyncById(object id)
    {
        return await Values.FirstOrDefaultAsync(IdEqualsPredicate);
    }
    public async Task<Result<IQueryable<T>>> GetPageAsync(int pageIndex, int pageSize)
    {
        return new(Values.GetPage(pageIndex, pageSize));
    }
    public async Task<Result<IQueryable<T>>> GetPageWhere(int pageIndex, int pageSize, Expression<Func<T, bool>> predicate)
    {
        return new(Values.GetPage(pageIndex, pageSize, predicate));
    }
    public Task<Result<Unit>> UpdateAsync(T userProfile)
    {
        return this.UpdateAsyncWithContext(userProfile, IdGetter(userProfile));
    }
}
