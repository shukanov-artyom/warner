using System;
using System.Collections.Generic;
using System.Linq;

namespace Warner.Analyzer.Repository
{
    internal class SvnBlameCache
    {
        private readonly Dictionary<string, IList<string>> cache =
            new Dictionary<string, IList<string>>();

        private readonly int size;

        public SvnBlameCache(int cacheSize)
        {
            if (cacheSize == 0)
            {
                throw new ArgumentException("cacheSize");
            }
            this.size = cacheSize;
        }

        public bool HasInfo(string key)
        {
            return cache.ContainsKey(key);
        }

        public IList<string> Get(string key)
        {
            if (cache.ContainsKey(key))
            {
                return cache[key];
            }
            return null;
        }

        public void Add(string key, IList<string> value)
        {
            if (cache.ContainsKey(key))
            {
                throw new InvalidOperationException(
                    "Cache already contains this key.");
            }

            if (cache.Keys.Count >= size)
            {
                string firstKey = cache.Keys.First();
                cache.Remove(firstKey);
            }
            cache[key] = value;
        }
    }
}
