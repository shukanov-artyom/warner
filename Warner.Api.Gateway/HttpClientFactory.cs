using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Warner.Api.Configuration;

namespace Warner.Api.Gateway
{
    internal class HttpClientFactory
    {
        private readonly WarnerApiConfiguration config;

        public HttpClientFactory(WarnerApiConfiguration config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public HttpClient GetClient()
        {
            var result = new HttpClient
            {
                BaseAddress = new Uri(config.ServiceUrl)
            };
            result.DefaultRequestHeaders.Accept.Clear();
            result.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return result;
        }
    }
}
