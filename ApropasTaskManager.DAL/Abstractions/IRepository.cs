using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.DAL.Abstractions
{
    public interface IRepository<T> where T : class
    {
        Task<Result<IQueryable<T>>> GetAllAsync();
        Task<Result<T>> GetAsyncById(object id);
        Task<Result<IQueryable<T>>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<Result<Unit>> CreateAsync(T item);
        Task<Result<Unit>> UpdateAsync(T item);
        Task<Result<Unit>> Delete(object id);
    }
}
