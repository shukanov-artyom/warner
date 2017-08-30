using System;

namespace Warner.Api.Cqrs.Infrastructure
{
    public interface IQueryResult
    {
        bool IsSuccess { get; }
    }
}
