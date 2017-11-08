using System;
using System.Threading.Tasks;
using AutoMapper;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Commands.WarningReport
{
    public class ReportWarningCommandHandler :
        IAsyncCommandHandler<ICommand<WarningReportCommandContext, WarningReportCommandResult>>
    {
        private readonly IBuildService buildService;
        private readonly IProjectService projectService;
        private readonly IWarningService warningService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ReportWarningCommandHandler(
            IProjectService projectService,
            IBuildService buildService,
            IWarningService warningService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.buildService = buildService
                ?? throw new ArgumentNullException(nameof(buildService));
            this.projectService = projectService
                ?? throw new ArgumentNullException(nameof(projectService));
            this.warningService = warningService
                ?? throw new ArgumentNullException(nameof(warningService));
            this.unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.mapper = mapper;
        }

        private WarningReportCommandContext CommandContext =>
            new WarningReportCommandContext(
                projectService,
                buildService,
                warningService,
                unitOfWork,
                mapper);

        public async Task<ICommandResult> ExecuteAsync(
            ICommand<WarningReportCommandContext, WarningReportCommandResult> command)
        {
            return await Task.Run(() => command.Execute(CommandContext));
        }

        public Task<ICommandResult> GetColdTask(ICommand<WarningReportCommandContext, WarningReportCommandResult> command)
        {
            return new Task<ICommandResult>(() => command.Execute(CommandContext));
        }
    }
}