using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Commands.WarningReport
{
    public class WarningReportCommandResult : ICommandResult
    {
        public WarningReportCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }

        public BuildWarning Warning { get; set; }

        public bool IsSuccess { get; private set; }
    }
}
