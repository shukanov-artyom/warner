using System;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.Commands.AddBuild
{
    public class AddBuildCommandResult : ICommandResult
    {
        public AddBuildCommandResult(
            bool isSuccess,
            long newEntityId)
        {
            IsSuccess = isSuccess;
            NewEntityId = newEntityId;
        }

        public bool IsSuccess { get; }

        public long NewEntityId { get; }
    }
}