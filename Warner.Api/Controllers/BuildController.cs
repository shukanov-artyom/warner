using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warner.Api.Commands.AddBuild;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Queries.GetAllBuildsForProject;
using Warner.Api.Queries.GetBuld;
using Warner.Domain;

namespace Warner.Api.Controllers
{
    public class BuildController : BaseController
    {
        public BuildController(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher)
            : base(commandDispatcher, queryDispatcher)
        {
        }

        [Route("api/Build/{id}")]
        [HttpGet]
        public async Task<Build> Get(long id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Cannot get object with Id = 0.");
            }
            GetBuildQuery query = new GetBuildQuery(id);
            GetBuildQueryResult result = await Run(query) as GetBuildQueryResult;
            return result.Build;
        }

        [Route("api/Build")]
        [HttpPost]
        public async Task<long> Post([FromBody] Build newBuild)
        {
            if (!newBuild.IsNew)
            {
                throw new InvalidOperationException(
                    "Provided object is not new.");
            }
            newBuild.BuildDate = DateTime.Now; // major dirty workaround not to transfer datetime in JSON
            AddBuildCommand command = new AddBuildCommand(newBuild);
            AddBuildCommandResult result = await Execute(command);
            return result.NewEntityId;
        }

        [HttpGet]
        [Route("api/Build/All/{projectName}")]
        public async Task<List<Build>> All(string projectName)
        {
            GetAllBuildsForProjectQuery query;
            int projectId;
            if (Int32.TryParse(projectName, out projectId))
            {
                query = new GetAllBuildsForProjectQuery(projectId);
            }
            else
            {
                query = new GetAllBuildsForProjectQuery(projectName);
            }
            GetAllBuildsForProjectQueryResult result =
                await Run(query) as GetAllBuildsForProjectQueryResult;
            return result.Builds;
        }

//        [HttpGet]
//        [Route("api/Duild/Blame/{buildId}")]
//        public async Task<BuildBlameInfo> Blame(long buildId)
//        {
//            var query = new GetBlameQuery(buildId);
//            GetBlameQueryResult result = Run(query) as GetBlameQueryResult;
//            return result.Blame;
//        }
    }
}
