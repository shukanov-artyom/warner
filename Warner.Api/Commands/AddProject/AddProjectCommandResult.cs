using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Commands.AddProject
{
    public class AddProjectCommandResult : ICommandResult
    {
        public AddProjectCommandResult(Project added)
        {
            Project = added;
            IsSuccess = true;
        }

        public AddProjectCommandResult(bool isSuccess = false)
        {
            IsSuccess = isSuccess;
        }

        public Project Project { get; }

        public bool IsSuccess { get; }
    }
}
