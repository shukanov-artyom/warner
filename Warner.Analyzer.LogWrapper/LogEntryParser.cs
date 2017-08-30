using System;
using System.Linq;
using Warner.Analyzer.LogWrapper.WarnTypeParsers;

namespace Warner.Analyzer.LogWrapper
{
    public class LogEntryParser
    {
        private readonly string line;

        public LogEntryParser(string line)
        {
            this.line = line.ToLower();
        }

        public bool IsWarningEntry()
        {
            if (String.IsNullOrEmpty(line))
            {
                return false;
            }
            if (LooksLikeWarningEntry(line) && !Ignored(line))
            {
                return true;
            }
            return false;
        }

        public LogEntryInfo Parse(string repo)
        {
            WarningParserFactory factory = new WarningParserFactory(line);
            WarningParserBase concreteParser = factory.Create();
            try
            {
                return concreteParser.Parse(repo);
            }
            catch (Exception)
            {
                Console.WriteLine($"Unable to parse the following line: \r\n {line}");
                throw;
            }
        }

        private bool LooksLikeWarningEntry(string line)
        {
            return CodeWarningFootprints.All.Any(fp => line.Contains(fp));
        }

        private bool Ignored(string line)
        {
            if (line.ToUpper().Contains("MSBUILD : warning".ToUpper()) ||
                line.ToUpper().Contains("temporary asp.net files".ToUpper()))
            {
                return true;
            }
            return false;
        }
    }
}
