using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Warner.Analyzer.Repository
{
    internal class SvnBlameWrapper
    {
        private readonly string fileInRepositoryPath;
        private readonly string repositoryPath;

        public SvnBlameWrapper(string repositoryPath, string fileInRepositoryPath)
        {
            if (String.IsNullOrEmpty(fileInRepositoryPath))
            {
                throw new ArgumentNullException(nameof(fileInRepositoryPath));
            }
            if (String.IsNullOrEmpty(repositoryPath))
            {
                throw new ArgumentNullException(nameof(repositoryPath));
            }
            if (!Directory.Exists(repositoryPath))
            {
                throw new DirectoryNotFoundException(repositoryPath);
            }
            string combinedPath = Path.Combine(repositoryPath, fileInRepositoryPath);
            if (!File.Exists(combinedPath))
            {
                throw new FileNotFoundException(nameof(combinedPath));
            }
            this.fileInRepositoryPath = fileInRepositoryPath;
            this.repositoryPath = repositoryPath;
        }

        public IList<string> GetLines()
        {
            using (Process svnProcess = new Process())
            {
                svnProcess.StartInfo = new ProcessStartInfo()
                {
                    WorkingDirectory = repositoryPath,
                    FileName = "svn",
                    Arguments = $"blame -v {fileInRepositoryPath}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                svnProcess.Start();
                Task<List<string>> worker = // creating hot task for background process buffer read
                    Task.Run(() => ReadProcessBuffer(svnProcess));
                svnProcess.WaitForExit();
                if (svnProcess.ExitCode != 0)
                {
                    string message = svnProcess.StandardError.ReadToEnd();
                    Console.WriteLine($"Could not retrieve SVN data: {message}");
                    throw new InvalidOperationException(message);
                }
                try
                {
                    List<string> result = worker.Result;
                    return result;
                }
                catch (AggregateException ag)
                {
                    throw ag.InnerException;
                }
            }
        }

        private List<string> ReadProcessBuffer(Process process)
        {
            List<string> result = new List<string>();
            while (!process.StandardOutput.EndOfStream)
            {
                string line = process.StandardOutput.ReadLine();
                result.Add(line);
            }
            return result;
        }
    }
}
