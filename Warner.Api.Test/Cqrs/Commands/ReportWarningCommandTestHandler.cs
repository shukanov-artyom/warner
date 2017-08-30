using System;
using Warner.Api.Commands.WarningReport;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.Test.Cqrs.Commands
{
    internal class ReportWarningCommandTestHandler
        : ITestCommandHandler<WarningReportCommand>
    {
        public ICommandResult Handle(WarningReportCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
