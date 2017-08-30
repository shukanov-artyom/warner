using System;
using System.Collections.Generic;
using System.IO;

namespace Warner.Analyzer.Repository
{
    public class SvnRepositoryFileWrapper : IRepositoryWrapper
    {
        private readonly string localRepositoryPath;
        private readonly string repositoryFilePathName;

        private IList<string> lines = null;

        public SvnRepositoryFileWrapper(
            string localRepositoryPath,
            string repositoryFilePathName)
        {
            if (String.IsNullOrEmpty(localRepositoryPath))
            {
                throw new ArgumentNullException(nameof(localRepositoryPath));
            }
            if (String.IsNullOrEmpty(repositoryFilePathName))
            {
                throw new ArgumentNullException(nameof(repositoryFilePathName));
            }
            string combinedPath = Path.Combine(
                localRepositoryPath,
                repositoryFilePathName);
            if (!File.Exists(combinedPath))
            {
                throw new FileNotFoundException(combinedPath);
            }
            this.repositoryFilePathName = repositoryFilePathName;
            this.localRepositoryPath = localRepositoryPath;
        }

        public string LocalRepositoryFolderPath
        {
            get
            {
                return localRepositoryPath;
            }
        }

        public string this[int lineNumber] // 1-based number comes
        {
            get
            {
                if (lines == null)
                {
                    lines = new SvnBlameWrapper(
                        localRepositoryPath,
                        repositoryFilePathName).GetLines();
                }
                lineNumber--; // convert to zero-based index
                string line = lines[lineNumber];
                var parser = new SvnBlameLineParser(line);
                SvnBlameEntry blame = parser.Parse();
                if (blame == null)
                {
                    // undefined line met most likely
                    // TODO: introduce null object pattern or process this situation in another correct manner.
                    return String.Empty;
                }
                if (String.IsNullOrEmpty(blame.DeveloperName))
                {
                    throw new InvalidOperationException(
                        "Could not determine developer name from provided data.");
                }
                return blame.DeveloperName;
            }
        }
    }
}
