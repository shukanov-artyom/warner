using System;
using System.Collections.Generic;
using System.IO;
using Warner.Analyzer.LogWrapper;
using Warner.Domain;

namespace Warner.Firefighter
{
    public static class Program
    {
        private const string OutputFormat =
            @"Project {0} Produces a warning {2} in source file {1}";

        public static void Main(string[] args)
        {
            string logFileName = args[0];
            string localRepoFolder = args[1];
            Project fakeProject = new Project() { Id = 1 };
            Build fakeBuild = new Build(fakeProject.Id);
            IEnumerable<BuildWarning> log =
                new LogFileWrapper(logFileName, fakeBuild, localRepoFolder);
            foreach (BuildWarning entry in log)
            {
                int codeLineNumber = entry.CodeLineNumber;
                string fullSourcePathName = localRepoFolder + entry.SourceFileName;
                string warningType = entry.WarningType;
                ILineProcessor processor = new LineProcessorFactory(warningType)
                    .Create(fullSourcePathName, codeLineNumber);
                processor.Process();
            }
        }
    }
}
