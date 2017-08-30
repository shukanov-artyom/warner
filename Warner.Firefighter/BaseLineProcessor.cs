using System;
using System.Collections.Generic;
using System.IO;

namespace Warner.Firefighter
{
    internal abstract class BaseLineProcessor : ILineProcessor
    {
        // cache of processed
        private static readonly Dictionary<string, List<int>> processed =
            new Dictionary<string, List<int>>();

        private readonly string filePathName;
        private readonly int codeLineNumber;

        protected BaseLineProcessor(
            string filePathName,
            int codeLineNumber)
        {
            this.filePathName = filePathName;
            this.codeLineNumber = codeLineNumber;
        }

        protected Dictionary<string, List<int>> Processed => processed;

        protected string FilePathName => filePathName;

        protected int CodeLineNumber => codeLineNumber;

        /// <summary>
        /// Offset for special cases of moving line count down.
        /// Normally 0.
        /// </summary>
        protected int Offset { get; set; }

        public virtual void Process()
        {
            if (processed.ContainsKey(filePathName) &&
                processed[filePathName].Contains(codeLineNumber))
            {
                return;
            }
            var tempFile = Path.GetTempFileName();
            var allLines = File.ReadLines(filePathName);
            IList<string> linesToKeep = new List<string>();

            int counter = 0; // 1-based lines counting
            foreach (string line in allLines)
            {
                counter++;
                if (counter == codeLineNumber + Offset)
                {
                    string processedLine =
                        ProcessLineOfInterest(line);
                    if (processedLine != null)
                    {
                        linesToKeep.Add(processedLine);
                    }
                }
                else
                {
                    linesToKeep.Add(line);
                }
            }

            File.WriteAllLines(tempFile, linesToKeep);
            File.Delete(filePathName);
            if (File.Exists(filePathName))
            {
                throw new InvalidOperationException();
            }
            File.Move(tempFile, filePathName);
            if (!processed.ContainsKey(filePathName))
            {
                processed[filePathName] = new List<int>();
            }
            processed[filePathName].Add(codeLineNumber);
        }

        /// <summary>
        /// Contract: returns null if we neen to throw line away.
        /// </summary>
        protected abstract string ProcessLineOfInterest(string line);
    }
}
