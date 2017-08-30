using System;
using System.Text.RegularExpressions;

namespace Warner.Analyzer.LogWrapper.WarnTypeParsers
{
    internal class WarningParserCa : WarningParserBase
    {
        private const string RegexPatternLinenumber =
            @"build\t(?<date>[^\t]*)\t(?<filename>[^\(]*)\((?<linenumber>\d*)\):\swarning\s(?<warncode>\S*):";

        private const string RegexPatternLineColNumber =
            @"build\t(?<date>[^\t]*)\t(?<filename>[^\(]*)\((?<linenumber>\d*),(?<colnumber>\d*)\):\swarning\s(?<warncode>\S*):";

        public WarningParserCa(string line)
            : base(line)
        {
        }

        public override WarningType Type => WarningType.CA;

        public override LogEntryInfo Parse(string repo)
        {
            Regex regexLineNumber = new Regex(RegexPatternLinenumber);
            Regex regexLineColNumber = new Regex(RegexPatternLineColNumber);
            Match match;
            if (regexLineNumber.IsMatch(Line))
            {
                match = regexLineNumber.Match(Line);
            }
            else if (regexLineColNumber.IsMatch(Line))
            {
                match = regexLineColNumber.Match(Line);
            }
            else
            {
                throw new NotSupportedException(
                    $"Cannot parse following line: \r\n{Line}");
            }
            string date = match.Groups["date"].Value;
            string warnCode = match.Groups["warncode"].Value;
            string filename = match.Groups["filename"].Value;
            string lineNumber = match.Groups["linenumber"].Value;

            var relative = new RelativePath(repo);
            string relativeFilename = relative.GetFromAbsolute(filename);
            string cleanRelativeFilename = Cleanup(relativeFilename);
            return new LogEntryInfo()
            {
                LoggedDate = DateTime.Parse(date),
                CodeLineNumber = Int32.Parse(lineNumber),
                SourceFilePathName = cleanRelativeFilename,
                WarningType = warnCode
            };
        }
    }
}
