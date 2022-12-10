using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.DAL.Abstractions;
using ApropasTaskManager.Shared;
using Microsoft.EntityFrameworkCore;

namespace ApropasTaskManager.DAL.EF.Implementations;
internal static class IRepositoryExtensions
{
    public static async Task<Result<Unit>> UpdateAsync<T>(this IRepository<T> repository, DbContext dbContext, DbSet<T> values, T value, object id) where T: class
    {
        var result = await repository.GetAsyncById(id);

        if (!result)
        {
            return Result<Unit>.CreateError(result.Error);
        }

        values.Update(value);

        await dbContext.SaveChangesAsync();

        return Unit.Default;
    }


    public static async Task<Result<Unit>> DeleteAsync<T>(this IRepository<T> repository, DbContext dbContext, DbSet<T> values, object id, object error) where T : class
    {
        var value = (await repository.GetAsyncById(id)).Value;

        if (value == null)
        {
            return Result<Unit>.CreateError(error);
        }

        values.Remove(value);

        await dbContext.SaveChangesAsync();

        return Unit.Default;
    }

    public static async Task<Result<Unit>> CreateAsync<T>(this IRepository<T> repository, DbContext dbContext, DbSet<T> values, T value) where T : class
    {
        await values.AddAsync(value);
        await dbContext.SaveChangesAsync();

        return Unit.Default;
    }
}
