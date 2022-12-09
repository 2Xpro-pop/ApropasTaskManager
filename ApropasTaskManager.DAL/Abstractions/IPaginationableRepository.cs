using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.DAL.Abstractions
{
    public interface IPaginationableRepository<T> : IRepository<T> where T: class
    {
        Task<Result<IEnumerable<T>>> GetPageAsync(int pageIndex, int pageSize);
        Task<Result<IEnumerable<T>>> GetPageWhere(int pageIndex, int pageSize, Expression<Func<T, bool>> predicate);
    }
}
