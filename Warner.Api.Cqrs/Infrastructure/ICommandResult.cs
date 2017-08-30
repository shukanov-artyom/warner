using System;

namespace Warner.Api.Infrastructure.Cqrs
{
    public interface ICommandResult
    {
        bool IsSuccess { get; }
    }
}
