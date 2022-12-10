using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.DAL.Abstractions;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.DAL.EF.Implementations;
internal class MissionsRepository : BaseRepositoryWithDbContext<Mission>, IMissionsRepository
{
    public MissionsRepository(ApplicationContext db) : base(db)
    {
        Values = db.Missions;
        DeleteError = ServerDefaultResponses.MissionNotFound;
        IdEqualsPredicate = id => m => m.Equals(id);
        IdGetter = m => m.Id;
    }
}
