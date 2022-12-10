﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ApropasTaskManager.DAL.Abstractions;
using ApropasTaskManager.Shared;
using Microsoft.EntityFrameworkCore;

namespace ApropasTaskManager.DAL.EF.Implementations;
internal class UserProfilesRepository : BaseRepositoryWithDbContext<UserProfile>, IUserProfilesRepository
{
    public UserProfilesRepository(ApplicationContext db) : base(db)
    {
        Values = db.Profiles;
        DeleteError = ServerDefaultResponses.UserNotFound;
        IdEqualsPredicate = id => u => u.Equals(id);
        IdGetter = u => u.UserId;
    }

}
