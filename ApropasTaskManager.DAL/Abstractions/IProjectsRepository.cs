using System;
using System.Collections.Generic;
using System.Text;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.DAL.Abstractions
{
    public interface IProjectsRepository : IPaginationableRepository<Project>
    {
    }
}
