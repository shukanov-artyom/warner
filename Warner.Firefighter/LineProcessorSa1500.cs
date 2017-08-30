using System;

namespace Warner.Firefighter
{
    internal class LineProcessorSa1500 : BaseLineProcessor
    {
        public LineProcessorSa1500(string filePathName, int lineNumber)
            : base(filePathName, lineNumber)
        {
            Offset = Processed.ContainsKey(FilePathName) // when we replace we move down by one line in the same file.
                ? Processed[FilePathName].Count
                : 0;
        }

        protected override string ProcessLineOfInterest(string line)
        {
            if (String.IsNullOrEmpty(line))
            {
                return line;
            }
            line = line.TrimEnd();
            int leadingSpacesCount = CalculateLeadingSpacesCount(line);
            string leadingSpaces = leadingSpacesCount == 0 ?
                String.Empty : line.Substring(0, leadingSpacesCount - 1);
            if (line[line.Length - 1] == '{')
            {
                string lineTrim = line.TrimEnd('{').TrimEnd();
                line = String.Format("{0}{2}{3}{1}",
                    lineTrim,
                    "{",
                    System.Environment.NewLine,
                    leadingSpaces);
                return line;
            }
            else if (line == "} else")
            {
                return String.Format("}{0}else", System.Environment.NewLine);
            }
            else
            {
                return line;
            }
        }

        private int CalculateLeadingSpacesCount(string line)
        {
            if (String.IsNullOrEmpty(line))
            {
                return 0;
            }
            int count = 0;
            while (line[count] == ' ')
            {
                count++;
            }
            return count + 1;
        }
    }
}
