using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.DAL.Abstractions;
using ApropasTaskManager.Shared;
using Microsoft.AspNetCore.Identity;

namespace ApropasTaskManager.DAL.EF.Implementations;
public class EfUnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _db;
    private readonly UserManager<User> _userManager;

    public EfUnitOfWork(UserManager<User> userManager, ApplicationContext db)
    {
        _userManager = userManager;
        _db = db;
    }

    public IUsersRepository Users => ReturnAndCreateIfNull(ref _usersRepository, () => new(_userManager));
    private UsersRepository _usersRepository = null!;

    public IUserProfilesRepository UserProfiles => ReturnAndCreateIfNull(ref _userProfilesRepository, () => new(_db));
    private UserProfilesRepository _userProfilesRepository = null!;

    public IProjectsRepository Projects => throw new NotImplementedException();

    public IMissionsRepository Missions => throw new NotImplementedException();

    private static T ReturnAndCreateIfNull<T>(ref T field, Func<T> creator)
    {
        field ??= creator();
        return field;
    }
}
