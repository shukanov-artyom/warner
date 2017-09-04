using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Warner.Analyzer.CommandLine;
using Warner.Analyzer.Infrastructure;
using Warner.Analyzer.LogWrapper;
using Warner.Analyzer.Report;
using Warner.Analyzer.Repository;
using Warner.Domain;

namespace Warner.Analyzer.Commands
{
    public class ApplicationCommandReportBuild : ApplicationCommand, IDisposable
    {
        private readonly ApplicationConfiguration appConfiguration;
        private readonly IWarningReportService reportService;
        private readonly BlockingCollection<Task<List<BuildWarning>>> warningsQueue =
            new BlockingCollection<Task<List<BuildWarning>>>(30);

        // wait untill all processing threads will finish
        private ManualResetEvent[] events;

        public ApplicationCommandReportBuild(
            ApplicationConfiguration appConfiguration,
            IWarningReportService reportService)
            : base(ApplicationCommandType.ReportBuild)
        {
            this.appConfiguration = appConfiguration ??
                 throw new ArgumentNullException(nameof(appConfiguration));
            ValidateCommandOptions();
            this.reportService = reportService ??
                throw new ArgumentNullException(nameof(reportService));
        }

        public override CommandResult Execute()
        {
            CommandLineOptionsReportBuild commandLineOptions =
                appConfiguration.CommandLineOptions as CommandLineOptionsReportBuild;
            // check for project
            Project project =
                reportService.GetProject(commandLineOptions.ProjectName) ??
                reportService.SaveProject(new Project()
                {
                    Name = commandLineOptions.ProjectName
                });
            // Report new build here
            var newBuild = new Build(project.Id)
            {
                BuildDate = DateTime.Now,
                BuildNumber = commandLineOptions.BuildNumber,
                LogFileName = commandLineOptions.LogFilePathName
            };
            Build build = reportService.AddBuild(newBuild);

            int num = appConfiguration.ApiConfiguration.BlameDataRetrievalThreadsCount;
            events = new ManualResetEvent[num];
            for (int i = 0; i < num; i++)
            {
                // Starting threads to retrieve SVN data
                int index = i;
                Task.Factory.StartNew(() => StartRepositoryDataRetrieval(index));
            }

            try
            {
                var groups = new LogFileGroupedWrapper(
                    commandLineOptions.LogFilePathName,
                    build,
                    commandLineOptions.RepositoryLocalPath);
                foreach (WarningsGroup group in groups)
                {
                    Task<List<BuildWarning>> coldTask =
                        GetColdDeveloperRetrievalTask(group);
                    warningsQueue.Add(coldTask);
                    Console.WriteLine($"Enqueued group task for {group.SourceFileName}");
                }
                warningsQueue.CompleteAdding();
                Console.WriteLine("Finished queueing warnings.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Command execution failed: {e.Message}, {e.StackTrace}");
                return new CommandResult(false, e.Message);
            }
            ManualResetEvent.WaitAll(events);
            reportService.Finish();
            return CommandResult.Ok;
        }

        public void Dispose()
        {
            warningsQueue?.Dispose();
        }

        private void ValidateCommandOptions()
        {
            CommandLineOptionsReportBuild options =
                appConfiguration.CommandLineOptions as CommandLineOptionsReportBuild;
            if (!File.Exists(options.LogFilePathName))
            {
                throw new CommandExecutionException(
                    options.ConsoleCommand,
                    $"Build log file {options.LogFilePathName} does not exist.");
            }
            if (String.IsNullOrEmpty(options.ProjectName))
            {
                throw new CommandExecutionException(
                    options.ConsoleCommand,
                    $"Project name is not provided.");
            }
            if (options.BuildNumber == 0)
            {
                throw new CommandExecutionException(
                    options.ConsoleCommand,
                    $"Build number is not provided.");
            }
        }

        private void StartRepositoryDataRetrieval(int index)
        {
            events[index] = new ManualResetEvent(false);
            try
            {
                foreach (Task<List<BuildWarning>> task in
                    warningsQueue.GetConsumingEnumerable())
                {
                    try
                    {
                        if (!task.IsCanceled)
                        {
                            task.RunSynchronously();
                            List<BuildWarning> result = task.Result;
                            result.ForEach(r => reportService.ReportWarning(r));
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Task canceled before executing.");
                        throw;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Unhandled exception in processing thread: {e.Message}");
                        throw;
                    }
                }
            }
            finally
            {
                events[index].Set(); // Signalize finished
            }
        }

        private Task<List<BuildWarning>> GetColdDeveloperRetrievalTask(
            WarningsGroup groupToProcess)
        {
            var result = new Task<List<BuildWarning>>(
                () => FillDevelopersInfo(groupToProcess)
            );
            return result;
        }

        private List<BuildWarning> FillDevelopersInfo(WarningsGroup group)
        {
            CommandLineOptionsReportBuild options =
                appConfiguration.CommandLineOptions as CommandLineOptionsReportBuild;
            var completeWarnings = new List<BuildWarning>();
            var repoFile = new SvnRepositoryFileWrapper(
                    options.RepositoryLocalPath,
                    group.SourceFileName);
            foreach (BuildWarning warn in group.Warnings)
            {
                warn.DeveloperName = repoFile[warn.CodeLineNumber]; // 1-based select
                completeWarnings.Add(warn);
            }
            return completeWarnings;
        }
    }
}
