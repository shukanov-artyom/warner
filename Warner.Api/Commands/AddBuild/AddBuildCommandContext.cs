using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Commands.AddBuild
{
    public class AddBuildCommandContext : ICommandContext
    {
        public AddBuildCommandContext(
            IBuildService buildService,
            IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            BuildService = buildService;
        }

        public IUnitOfWork UnitOfWork
        {
            get;
        }

        public IBuildService BuildService
        {
            get;
        }
    }
}