using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.DAL.Abstractions;

namespace ApropasTaskManager.DAL.EF.Implementations;
public class EfUnitOfWork : IUnitOfWork
{
    public IUsersRepository Users => throw new NotImplementedException();

    public IUserProfilesRepository UserProfiles => throw new NotImplementedException();

    public IProjectsRepository Projects => throw new NotImplementedException();

    public IMissionsRepository Missions => throw new NotImplementedException();

    private static T ReturnAndCreateIfNull<T>(ref T field, Func<T> creator)
    {
        field ??= creator();
        return field;
    }
}
