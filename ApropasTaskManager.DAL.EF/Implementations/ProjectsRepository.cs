using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.DAL.Abstractions;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.DAL.EF.Implementations;
internal class ProjectsRepository : BaseRepositoryWithDbContext<Project>, IProjectsRepository
{
    public ProjectsRepository(ApplicationContext db) : base(db)
    {
        Values = db.Projects;
        DeleteError = ServerDefaultResponses.ProjectNotFound;
        IdEqualsPredicate = id => p => p.Id.Equals(id);
        IdGetter = p => p.Id;
    }
}
