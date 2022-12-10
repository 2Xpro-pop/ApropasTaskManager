using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApropasTaskManager.DAL.EF;
public class ApplicationContext : IdentityDbContext<User>
{
    public DbSet<UserProfile> Profiles { get; set; } = null!;
    public DbSet<Mission> Missions { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }


}
