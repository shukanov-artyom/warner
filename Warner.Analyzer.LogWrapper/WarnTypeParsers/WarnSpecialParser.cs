using System;

namespace Warner.Analyzer.LogWrapper.WarnTypeParsers
{
    internal abstract class WarningParserBase
    {
        public WarningParserBase(string line)
        {
            if (String.IsNullOrEmpty(line))
            {
                throw new ArgumentException("line");
            }
            Line = line;
        }

        protected string Line { get; private set; }

        public abstract WarningType Type { get; }

        public abstract LogEntryInfo Parse(string repo);

        protected string Cleanup(string relativeFilename)
        {
            return relativeFilename;
            //            if (!relativeFilename.Contains("`"))
            //            {
            //                return relativeFilename;
            //            }
            //            int indexOf = relativeFilename.IndexOf("`");
            //            string substr = relativeFilename.Substring(indexOf, 2);
            //            string result = relativeFilename.Replace(substr, String.Empty);
            //            return result;
        }
    }
}
