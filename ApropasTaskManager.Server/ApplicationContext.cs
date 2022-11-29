using ApropasTaskManager.Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApropasTaskManager.Server;

public class ApplicationContext: IdentityDbContext<User>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
    {
        
    }
}
