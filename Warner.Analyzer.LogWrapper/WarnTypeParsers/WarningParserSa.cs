using System;
using System.Text.RegularExpressions;

namespace Warner.Analyzer.LogWrapper.WarnTypeParsers
{
    internal class WarningParserSa : WarningParserBase
    {
        private const string pattern =
            @"build\t(?<date>[^\t]*)\t\s*(?<filename>[^\(]*)\((?<linenumber>\d*),(?<colnumber>[^:]*)\):\swarning\s:\s(?<warncode>\S*)\s:";

        public WarningParserSa(string line)
            : base(line)
        {
        }

        public override WarningType Type => WarningType.SA;

        public override LogEntryInfo Parse(string repo)
        {
            Regex regex = new Regex(pattern);
            Match match = regex.Match(Line);
            string date = match.Groups["date"].Value;
            string warnCode = match.Groups["warncode"].Value.ToLower();
            string filename = match.Groups["filename"].Value;
            string lineNumber = match.Groups["linenumber"].Value;
            var relative = new RelativePath(repo);
            return new LogEntryInfo()
            {
                LoggedDate = DateTime.Parse(date),
                CodeLineNumber = Int32.Parse(lineNumber),
                SourceFilePathName = Cleanup(relative.GetFromAbsolute(filename)),
                WarningType = warnCode
            };
        }
    }
}
