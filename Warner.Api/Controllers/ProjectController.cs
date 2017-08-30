using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warner.Api.Commands.AddProject;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Queries;
using Warner.Api.Queries.GetAllProjects;
using Warner.Domain;

namespace Warner.Api.Controllers
{
    //[Route("api/Project")]
    public class ProjectController : BaseController
    {
        public ProjectController(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher)
            : base(commandDispatcher, queryDispatcher)
        {
        }

        [Route("api/Project/{name}")]
        [HttpGet]
        public async Task<Project> Get(string name)
        {
            var query = new GetProjectQuery(name);
            IQueryResult result = await Run(query);
            var typedResult = result as GetProjectQueryResult;
            if (typedResult == null)
            {
                throw new InvalidOperationException("Wrong command result!");
            }
            return typedResult.Project;
        }

        [Route("api/Project")]
        [HttpPost]
        public async Task<Project> Post([FromBody] Project newProject)
        {
            if (!newProject.IsNew)
            {
                throw new InvalidOperationException("Object is not new!");
            }
            var command = new AddProjectCommand(newProject);
            AddProjectCommandResult commandResult = await Execute(command);
            return commandResult.Project;
        }

        [Route("api/Project/All")]
        [HttpGet]
        public async Task<List<Project>> GetAll()
        {
            var query = new GetAllProjectsQuery();
            var result = await Run(query) as GetAllProjectsQueryResult;
            return result.Projects;
        }
    }
}
