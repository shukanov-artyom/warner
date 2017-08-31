using System;
using System.Collections.Generic;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.Queries.BuildWarningsMovement
{
    public class BuildWarningsMovementQueryResult : IQueryResult
    {
        private readonly IDictionary<string, int> movement;

        public BuildWarningsMovementQueryResult(IDictionary<string, int> movement)
        {
            IsSuccess = true;
            this.movement = movement;
        }

        public bool IsSuccess { get; }

        public IDictionary<string, int> Movement => movement;
    }
}