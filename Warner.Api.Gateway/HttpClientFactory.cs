using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Warner.Api.Gateway
{
    internal class HttpClientFactory
    {
        private readonly string serviceUrl;

        public HttpClientFactory(string serviceUrl)
        {
            this.serviceUrl = serviceUrl;
        }

        public HttpClient GetClient()
        {
            var result = new HttpClient
            {
                BaseAddress = new Uri(serviceUrl)
            };
            result.DefaultRequestHeaders.Accept.Clear();
            result.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return result;
        }
    }
}
