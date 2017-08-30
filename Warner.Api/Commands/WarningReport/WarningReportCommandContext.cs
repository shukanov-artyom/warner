using System;
using AutoMapper;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Commands.WarningReport
{
    public class WarningReportCommandContext : ICommandContext
    {
        public WarningReportCommandContext(
            IProjectService projectService,
            IBuildService buildService,
            IWarningService warningService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            Projects = projectService
                ?? throw new ArgumentNullException(nameof(projectService));
            Builds = buildService
                ?? throw new ArgumentNullException(nameof(buildService));
            Warnings = warningService
                ?? throw new ArgumentNullException(nameof(warningService));
            UnitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
            Mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IProjectService Projects
        {
            get;
        }

        public IBuildService Builds
        {
            get;
        }

        public IWarningService Warnings
        {
            get;
        }

        public IUnitOfWork UnitOfWork
        {
            get;
        }

        public IMapper Mapper
        {
            get;
        }
    }
}
