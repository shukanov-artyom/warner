using System;
using System.Collections.Generic;
using Warner.Domain;

namespace Warner.Api.Services
{
    public interface IProjectService
    {
        Project GetByName(string name);

        Project SaveNew(Project project);

        List<Project> GetAll();
    }
}
