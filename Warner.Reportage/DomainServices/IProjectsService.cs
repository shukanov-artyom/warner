using System;
using System.Collections.Generic;
using Warner.Domain;

namespace Warner.Reportage.DomainServices
{
    public interface IProjectsService
    {
        IEnumerable<Project> GetAll();
    }
}
