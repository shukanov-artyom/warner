using System;
using Warner.Domain;

namespace Warner.Analyzer.Repository
{
    public interface IRepositoryWrapper
    {
        string LocalRepositoryFolderPath
        {
            get;
        }

        /// <summary>
        /// Returns developer names.
        /// TODO : refactor to make more clear.
        /// </summary>
        string this[int line]
        {
            get;
        }
    }
}
