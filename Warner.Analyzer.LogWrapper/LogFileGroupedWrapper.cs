using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Warner.Analyzer.Commands;
using Warner.Domain;

namespace Warner.Analyzer.LogWrapper
{
    public class LogFileGroupedWrapper : IEnumerable<WarningsGroup>
    {
        private readonly string logFile;
        private readonly Build build;
        private readonly string repoFolder;

        public LogFileGroupedWrapper(
            string logFile,
            Build build,
            string repoFolder)
        {
            this.build = build;
            this.logFile = logFile;
            this.repoFolder = repoFolder;
        }

        public IEnumerator<WarningsGroup> GetEnumerator()
        {
            WarningsGroup group = null;
            foreach (BuildWarning warn in
                new LogFileWrapper(logFile, build, repoFolder))
            {
                if (group == null || group.SourceFileName != warn.SourceFileName)
                {
                    if (group != null)
                    {
                        yield return group;
                    }
                    ValidateSourceFileName(Path.Combine(repoFolder, warn.SourceFileName));
                    group = new WarningsGroup
                    {
                        SourceFileName = warn.SourceFileName
                    };
                }
                group.Warnings.Add(warn);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static void ValidateSourceFileName(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Source file not found");
            }
        }
    }
}
