using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Test.Cqrs.Commands;
using Warner.Api.Test.Cqrs.Queries;

namespace Warner.Api.Test
{
    public class CqrsUnitTestClass
    {
        protected CqrsUnitTestClass()
        {
            CommandDispatcher = new TestCommandDispatcher();
            QueryDispatcher = new TestQueryDispatcher();
        }

        private TestCommandDispatcher CommandDispatcher { get; }

        private TestQueryDispatcher QueryDispatcher { get; }

        public ICommandResult Execute(ICommand command)
        {
            return CommandDispatcher.Submit(command);
        }

        public IQueryResult Run(IQuery query)
        {
            return QueryDispatcher.Submit(query);
        }
    }
}
