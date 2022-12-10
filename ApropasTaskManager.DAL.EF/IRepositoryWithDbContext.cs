using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.DAL.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ApropasTaskManager.DAL.EF;
/// <summary>
/// Need for extensions
/// </summary>
/// <typeparam name="T"></typeparam>
internal interface IRepositoryWithDbContext<T>: IRepository<T> where T: class
{
    public DbContext Context { get; }
    public DbSet<T> Values { get; }
}
