using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Warner.Domain;

namespace Warner.Analyzer.LogWrapper
{
    public class LogFileWrapper : IEnumerable<BuildWarning>
    {
        private readonly string filePathName;
        private readonly Build build;
        private readonly string localRepoFolder;

        public LogFileWrapper(
            string filePathName,
            Build build,
            string localRepoFolder)
        {
            if (String.IsNullOrEmpty(filePathName))
            {
                throw new ArgumentNullException(nameof(filePathName));
            }
            this.build = build ?? throw new ArgumentNullException(nameof(build));
            this.localRepoFolder = localRepoFolder;
            this.filePathName = filePathName;
        }

        public IEnumerator<BuildWarning> GetEnumerator()
        {
            int lineCounter = 0;
            using (var file = new FileStream(
                filePathName,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite))
            using (var reader = new StreamReader(file))
            {
                do
                {
                    string line = reader.ReadLine();
                    if (line.ToUpper().Contains("Warner.Analyzer".ToUpper()))
                    {
                        // We're finished as we are analyzing already.
                        yield break;
                    }
                    lineCounter++;
                    if (String.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    var parser = new LogEntryParser(line);
                    if (!parser.IsWarningEntry())
                    {
                        continue;
                    }
                    LogEntryInfo warning = parser.Parse(localRepoFolder);
                    ValidateSourceFileExistence(localRepoFolder,
                        warning.SourceFilePathName);
                    BuildWarning result = new BuildWarning
                    {
                        WarningType = warning.WarningType,
                        CodeLineNumber = warning.CodeLineNumber,
                        SourceFileName = warning.SourceFilePathName,
                        BuildId = build.Id,
                        LogLineNumber = lineCounter
                    };
                    yield return result;
                }
                while (!reader.EndOfStream);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ValidateSourceFileExistence(string one, string two)
        {
            string path = Path.Combine(one, two);
            if (!File.Exists(path))
            {
                // let's doublecheck
                Console.WriteLine($"Validating suspect path {path}");
                using (var file = File.OpenRead(path))
                {
                    // if it opens then some permissions issue apparently.
                    return;
                }
                throw new FileNotFoundException($"File does not exist: {path}");
            }
        }
    }
}
