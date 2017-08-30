using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Warner.Domain;
using System.Threading.Tasks;
using Warner.Api.Commands.WarningReport;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Queries.BuildWarningSummary;
using Warner.Api.Queries.GetAllWarningsForBuild;

namespace Warner.Api.Controllers
{
    [Route("api/Warning")]
    public class WarningController : BaseController
    {
        public WarningController(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher)
            : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpPost]
        public async Task Post([FromBody]BuildWarning[] warnings)
        {
            foreach (BuildWarning warning in warnings)
            {
                var command = new WarningReportCommand(warning);
                ICommandResult result = await Execute(command);
                if (!result.IsSuccess)
                {
                    throw new InvalidOperationException(
                        "Command execution failed.");
                }
            }
        }

        [Route("All/{buildId}")]
        [HttpGet]
        public async Task<IEnumerable<BuildWarning>> GetAll(long buildId)
        {
            var query = new GetAllWarningsForBuildQuery(buildId);
            var result = await Run(query) as GetAllWarningsForBuildQueryResult;
            return result.Warnings;
        }

        [Route("Summary/{buildId}")]
        [HttpGet]
        public async Task<IDictionary<string, int>> Summary(long buildId)
        {
            var query = new BuildWarningSummaryQuery(buildId);
            var result = await Run(query) as BuildWarningSummaryQueryResult;
            return result.Dictionary;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello world.";
        }
    }
}
