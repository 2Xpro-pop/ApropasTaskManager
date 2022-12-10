using ApropasTaskManager.DAL.Abstractions;
using ApropasTaskManager.DAL.EF.Implementations;
using ApropasTaskManager.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApropasTaskManager.DAL.EF;
public static class DalServiceCollectionExtensions
{
    public static void AddDalEf(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptions)
    {
        services.AddDbContext<ApplicationContext>(dbContextOptions);

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
    }
}
