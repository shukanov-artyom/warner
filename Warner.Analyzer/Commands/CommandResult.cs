using System;

namespace Warner.Analyzer.Commands
{
    public class CommandResult
    {
        private static CommandResult okResult = new CommandResult(
            true,
            "Command completed successfully.");

        public CommandResult(
            bool isSuccess,
            string message)
        {
            IsSuccess = isSuccess;
            ErrorMessage = message;
        }

        public bool IsSuccess { get; private set; }

        public string ErrorMessage { get; private set; }

        public static CommandResult Ok
        {
            get
            {
                return okResult;
            }
        }
    }
}
