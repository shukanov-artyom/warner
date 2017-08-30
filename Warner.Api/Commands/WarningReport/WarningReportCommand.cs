using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Commands.WarningReport
{
    public class WarningReportCommand :
        ICommand<WarningReportCommandContext, WarningReportCommandResult>
    {
        private readonly BuildWarning warning;

        public WarningReportCommand(BuildWarning warning)
        {
            this.warning = warning ??
                throw new ArgumentNullException("warning");
        }

        public BuildWarning Warning => warning;

        public ICommandResult Execute(ICommandContext context)
        {
            var commandContext = context as WarningReportCommandContext
                ?? throw new ArgumentException("Wrong command context type.");
            if (Warning == null)
            {
                throw new InvalidOperationException("Warning not set.");
            }

            BuildWarning result = commandContext.Warnings.SaveNew(Warning);

            commandContext.UnitOfWork.SubmitChanges();
            return new WarningReportCommandResult(isSuccess: true)
            {
                Warning = result
            };
        }

        public WarningReportCommandResult
            Execute(WarningReportCommandContext context)
        {
            return Execute(context as ICommandContext) as WarningReportCommandResult;
        }
    }
}
