using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Warner.Api.Commands;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.Controllers
{
    /// <summary>
    /// Keeps Command / Query dispatching
    /// responsibilities common for all controllers.
    /// </summary>
    public class BaseController : Controller
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;

        public BaseController(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher)
        {
            this.commandDispatcher = commandDispatcher
                ?? throw new ArgumentNullException(nameof(commandDispatcher));
            this.queryDispatcher = queryDispatcher
                ?? throw new ArgumentNullException(nameof(queryDispatcher));
        }

        /// <summary>
        /// Executes a command providing a convenient execution syntax using
        /// C# 3 type inference features.
        /// Domain Layer entrypoint for Command.
        /// </summary>
        /// <typeparam name="TCommandContext">Required Context class for this type of command.</typeparam>
        /// <typeparam name="TCommandResult">Type of a result of this command.</typeparam>
        /// <param name="command">Type of a command to be processed.</param>
        /// <returns>Command result instance.</returns>
        protected async Task<TCommandResult> Execute<TCommandContext, TCommandResult>(
            ICommand<TCommandContext, TCommandResult> command)
            where TCommandResult : class, ICommandResult
            where TCommandContext : class, ICommandContext
        {
            return await commandDispatcher.Submit(command) as TCommandResult;
        }

        /// <summary>
        /// Runs Query.
        /// Domain Layer entrypoint for Query.
        /// </summary>
        protected async Task<IQueryResult> Run<TQuery>(TQuery query)
            where TQuery : IQuery
        {
            return await queryDispatcher.Submit(query);
        }
    }
}
