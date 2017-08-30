using System;

namespace Warner.Analyzer.LogWrapper.WarnTypeParsers
{
    public class RelativePath
    {
        public RelativePath(string repo)
        {
            Repo = repo;
        }

        private string Repo { get; set; }

        /// <summary>
        /// Removes repository Path;
        /// </summary>
        public string GetFromAbsolute(string abs)
        {
            abs = abs.Trim();
            string repo = Repo.Trim();
            if (!abs.ToLower().StartsWith(repo.ToLower()))
            {
                // Assume this is already a relative one!
                return abs;
            }
            return abs.Trim().Substring(repo.Length);
        }
    }
}
