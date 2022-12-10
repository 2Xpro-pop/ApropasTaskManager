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
internal static class IRepositoryWithDbContextExtensions
{
    public static async Task<Result<Unit>> UpdateAsyncWithContext<T>(this IRepositoryWithDbContext<T> repository, T value, object id) where T: class
    {
        var result = await repository.GetAsyncById(id);

        if (!result)
        {
            return Result<Unit>.CreateError(result.Error);
        }

        repository.Values.Update(value);

        await repository.Context.SaveChangesAsync();

        return Unit.Default;
    }


    public static async Task<Result<Unit>> DeleteAsyncWithContext<T>(this IRepositoryWithDbContext<T> repository, object id, object error) where T : class
    {
        var value = (await repository.GetAsyncById(id)).Value;

        if (value == null)
        {
            return Result<Unit>.CreateError(error);
        }

        repository.Values.Remove(value);

        await repository.Context.SaveChangesAsync();

        return Unit.Default;
    }

    public static async Task<Result<Unit>> CreateAsyncWithContext<T>(this IRepositoryWithDbContext<T> repository, T value) where T : class
    {
        await repository.Values.AddAsync(value);
        await repository.Context.SaveChangesAsync();

        return Unit.Default;
    }
}
