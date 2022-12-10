using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared;
using Microsoft.EntityFrameworkCore;

namespace ApropasTaskManager.DAL.EF.Implementations;
internal static class DbSetExtensions
{
    public static IQueryable<T> GetPage<T>(this DbSet<T> values, int pageIndex, int pageSize) where T: class
    {
        return values.Skip(pageIndex * pageSize)
                     .Take(pageSize);
    }

    public static IQueryable<T> GetPage<T>(this DbSet<T> values, int pageIndex, int pageSize, Expression<Func<T, bool>> predicate) where T : class
    {
        return values.GetPage(pageIndex, pageSize)
                     .Where(predicate);
    }

    public static Result<IQueryable<T>> ResultedWhere<T>(this DbSet<T> values, int pageIndex, int pageSize, Expression<Func<T, bool>> predicate) where T : class
    {
        return new (
            values.Where(predicate)
        );
    }

}
