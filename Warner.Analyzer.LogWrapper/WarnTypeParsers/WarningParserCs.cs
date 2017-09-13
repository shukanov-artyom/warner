using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Warner.Analyzer.LogWrapper.WarnTypeParsers
{
    internal class WarningParserCs : WarningParserBase
    {
        private const string DistinguishPattern =
            @".cs\(\d+,\d+\)";

        private const string PatternType1 =
            @"build\t(?<date>[^\t]*)\s+(?<filename>[^\(]*)\((?<linenumber>\d*),(?<colnumber>\d*)\):\swarning\s(?<warncode>\S*):(?<Gist>.+)\s\[(?<csproj>.+)\]";

        private const string PatternType2 =
            @"build\t(?<date>[^\t]*)\s+(?<filename>[^\(]*)\((?<linenumber>\d*)\):\swarning\s(?<warncode>\S*):";

        private readonly CsWarningType csType;

        public WarningParserCs(string line)
            : base(line)
        {
            Regex typeDefinitionRegex = new Regex(DistinguishPattern);
            if (typeDefinitionRegex.IsMatch(Line))
            {
                csType = CsWarningType.Type1;
            }
            else
            {
                csType = CsWarningType.Type2;
            }
        }

        private enum CsWarningType
        {
            Type1,
            Type2
        }

        public override WarningType Type => WarningType.CS;

        public override LogEntryInfo Parse(string repo)
        {
            Regex regex = csType == CsWarningType.Type1 ?
                new Regex(PatternType1) :
                new Regex(PatternType2);
            Match match = regex.Match(Line);
            string date = match.Groups["date"].Value;
            string warnCode = match.Groups["warncode"].Value.ToLower();
            string filename = match.Groups["filename"].Value;
            string lineNumber = match.Groups["linenumber"].Value;
            string csproj = match.Groups["csproj"].Value;

            string filePath = filename;
            if (csType == CsWarningType.Type1)
            {
                filePath = ComposePathsInProject(repo, csproj, filePath);
            }
            if (csType == CsWarningType.Type2)
            {
                var relative = new RelativePath(repo);
                filePath = relative.GetFromAbsolute(filePath);
            }
            return new LogEntryInfo()
            {
                LoggedDate = DateTime.Parse(date),
                CodeLineNumber = Int32.Parse(lineNumber),
                SourceFilePathName = Cleanup(filePath),
                WarningType = warnCode
            };
        }

        private string ComposePathsInProject(
            string repo,
            string csproj,
            string filename)
        {
            string projectFolder = Path.GetDirectoryName(csproj);
            string project = projectFolder.Substring(repo.Length);
            //            string[] split = projectFolder.Split(Path.DirectorySeparatorChar);
            //            string projectFolderName = split[split.Length - 1];
            string result = Path.Combine(project, filename);
            return result;
        }
    }
}
