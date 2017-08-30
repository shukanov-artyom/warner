using System;

namespace Warner.Api.Cqrs.Infrastructure
{
    public interface ICommandResult
    {
        bool IsSuccess { get; }
    }
}
