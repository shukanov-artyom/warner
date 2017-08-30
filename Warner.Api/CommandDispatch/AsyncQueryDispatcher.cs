using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.CommandHandlers
{
    public class AsyncQueryDispatcher : IQueryDispatcher
    {
        private readonly BlockingCollection<Task<IQueryResult>> queue =
            new BlockingCollection<Task<IQueryResult>>();

        private readonly ILifetimeScope container;

        public AsyncQueryDispatcher(ILifetimeScope container)
        {
            Task.Factory.StartNew(Run);
            this.container = container;
        }

        /// <summary>
        /// Submits query for execution.
        /// </summary>
        public Task<IQueryResult> Submit<TQuery>(TQuery query)
            where TQuery : IQuery
        {
            var handler = container.Resolve<IAsyncQueryHandler<TQuery>>();
            Task<IQueryResult> coldTask = handler.GetColdTask(query);
            queue.Add(coldTask); // do not start the task here.
            return coldTask;
        }

        private void Run()
        {
            foreach (Task<IQueryResult> task in queue.GetConsumingEnumerable())
            {
                try
                {
                    if (!task.IsCanceled)
                    {
                        task.RunSynchronously();
                    }
                }
                catch (InvalidOperationException)
                {
                    Debug.WriteLine("Task canceled before executing.");
                }
            }
        }
    }
}
