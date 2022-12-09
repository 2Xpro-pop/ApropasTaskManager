using System;
using System.Collections.Generic;
using System.Text;

namespace ApropasTaskManager.DAL.Abstractions
{
    public interface IUnitOfWork
    {
        IUsersRepository Users { get; }
        IUserProfilesRepository UserProfiles { get; }
        IProjectsRepository Projects { get; }
        IMissionsRepository Missions { get; }
    }
}
