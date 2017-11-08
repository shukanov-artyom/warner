using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.CommandDispatch
{
    public class AsyncCommandDispatcher : ICommandDispatcher, IDisposable
    {
        private readonly BlockingCollection<Task<ICommandResult>> commandsQueue =
            new BlockingCollection<Task<ICommandResult>>();

        private readonly ILifetimeScope container;

        public AsyncCommandDispatcher(
            ILifetimeScope container)
        {
            this.container = container ??
                throw new ArgumentNullException(nameof(container));
            Task.Factory.StartNew(Start);
        }

        public void Dispose()
        {
            commandsQueue.CompleteAdding();
        }

        // puts command into commands queue, returns awaitable.
        public Task<ICommandResult> Submit<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var handler = container.Resolve<IAsyncCommandHandler<TCommand>>();
            Task<ICommandResult> coldTask = handler.GetColdTask(command);
            commandsQueue.Add(coldTask); // do not start the task here.
            return coldTask;
        }

        private void Start()
        {
            foreach (Task<ICommandResult> task in
                commandsQueue.GetConsumingEnumerable())
            {
                try
                {
                    if (!task.IsCanceled)
                    {
                        // This is the place where we actually run payload code.
                        task.RunSynchronously();
                    }
                }
                catch (InvalidOperationException)
                {
                    // rare case when task is canceled after cancel check
                    Debug.WriteLine("Task canceled before executing.");
                }
            }
        }
    }
}
