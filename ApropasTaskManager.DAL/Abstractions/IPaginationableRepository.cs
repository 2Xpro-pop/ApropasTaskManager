using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.DAL.Abstractions
{
    public interface IPaginationableRepository<T> : IRepository<T> where T: class
    {
        Task<Result<IQueryable<T>>> GetPageAsync(int pageIndex, int pageSize);
        Task<Result<IQueryable<T>>> GetPageWhere(int pageIndex, int pageSize, Expression<Func<T, bool>> predicate);
    }
}
