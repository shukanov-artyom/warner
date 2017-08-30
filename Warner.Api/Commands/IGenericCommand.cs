using System;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.Commands
{
    /// <summary>
    /// Generic command interface.
    /// </summary>
    /// <typeparam name="TCommandContext">Contravariant in parameter.</typeparam>
    /// <typeparam name="TCommandResult">Covariant out parameter.</typeparam>
    public interface ICommand<in TCommandContext, out TCommandResult> : ICommand
        where TCommandContext : ICommandContext
        where TCommandResult : ICommandResult
    {
        /// <summary>
        /// Executes command.
        /// Contains command domain logic.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        TCommandResult Execute(TCommandContext context);
    }
}
