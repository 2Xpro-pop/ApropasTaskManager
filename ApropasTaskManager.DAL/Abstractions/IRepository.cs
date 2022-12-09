using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.DAL.Abstractions
{
    public interface IRepository<T> where T : class
    {
        Task<Result<IEnumerable<T>>> GetAllAsync();
        Task<Result<T>> GetAsyncById(object id);
        Task<Result<IEnumerable<T>>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<Result<Unit>> CreateAsync(T item);
        Task<Result<Unit>> UpdateAsync(T item);
        Task<Result<Unit>> Delete(object id);
    }
}
