using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Warner.Firefighter
{
    internal class LineProcessorSa1058 : BaseLineProcessor
    {
        public LineProcessorSa1058(string filePathName,
            int codeLineNumber)
            : base(filePathName, codeLineNumber)
        {
        }

        public override void Process()
        {
            if (Processed.ContainsKey(FilePathName) &&
                Processed[FilePathName].Contains(CodeLineNumber))
            {
                return;
            }
            var tempFile = Path.GetTempFileName();
            var allLines = File.ReadLines(FilePathName);
            IList<string> linesToKeep = new List<string>();

            int counter = 0; // 1-based lines counting
            foreach (string line in allLines)
            {
                counter++;
                if (counter != CodeLineNumber - 1)
                {
                    linesToKeep.Add(line);
                }
            }

            File.WriteAllLines(tempFile, linesToKeep);

            File.Delete(FilePathName);
            File.Move(tempFile, FilePathName);
            Processed[FilePathName].Add(CodeLineNumber);
        }

        protected override string ProcessLineOfInterest(string line)
        {
            throw new NotSupportedException();
        }
    }
}
