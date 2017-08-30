using System;
using Microsoft.Extensions.Configuration;

namespace Warner.Api.Configuration
{
    public class WarnerApiConfiguration
    {
        private readonly IConfigurationRoot config;

        public WarnerApiConfiguration(IConfigurationRoot configRoot)
        {
            this.config = configRoot ??
                throw new ArgumentNullException(nameof(configRoot));
        }

        public int BlameDataRetrievalThreadsCount
        {
            get
            {
                string line = config[
                    "warner.api:multithreading:BlameDataRetrievalThreadsCount"];
                if (!Int32.TryParse(line, out int result))
                {
                    result = 4;
                }
                return result;
            }
        }

        public string ServiceUrl
        {
            get
            {
                string line = config["warner.api:service:Url"];
                return line;
            }
        }
    }
}
