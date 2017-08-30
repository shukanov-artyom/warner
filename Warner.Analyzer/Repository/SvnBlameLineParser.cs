using System;
using System.Text.RegularExpressions;

namespace Warner.Analyzer.Repository
{
    public class SvnBlameLineParser
    {
        private const string RegexTemplate =
            @"\s+(?<Revision>\d+)\s+(?<DeveloperName>\S+)\s(?<ModificationDate>.{19})\s(?<TimeOffsetInfo>\+.+\(.{16}\))\s(?<LineContent>.*)";

        private const string UndefinedLineRegexTemplate =
            @"\s+-\s+-\s+-\s+.+";

        private readonly string line;

        public SvnBlameLineParser(string line)
        {
            if (String.IsNullOrEmpty(line))
            {
                throw new ArgumentNullException(nameof(line));
            }
            this.line = line;
        }

        public SvnBlameEntry Parse()
        {
            var result = new SvnBlameEntry();
            Regex regex = new Regex(RegexTemplate);
            Regex undefinedRegex = new Regex(UndefinedLineRegexTemplate);
            Match match = regex.Match(line);
            if (undefinedRegex.IsMatch(line))
            {
                return null;
            }
            else if (!regex.IsMatch(line))
            {
                throw new InvalidOperationException(
                    $"Cannot parse SVN blame output line: \n {line}");
            }

            string rev = match.Groups["Revision"].Value;
            int revision = Int32.Parse(rev);
            string developer = match.Groups["DeveloperName"].Value;
            string modDate = match.Groups["ModificationDate"].Value;
            DateTime date = DateTime.Parse(modDate);

            //string lineContent = match.Groups["LineContent"].Value;
            result.DeveloperName = developer;
            //result.LineContent = lineContent; // to save memory
            result.ModificationDate = date;
            result.Revision = revision;

            return result;
        }
    }
}
