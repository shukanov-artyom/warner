using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Warner.Api.Configuration;

namespace Warner.Api.Gateway
{
    public abstract class WarnerServiceBase
    {
        protected WarnerServiceBase(WarnerApiConfiguration config)
        {
            Config = config
                ?? throw new ArgumentNullException(nameof(config));
        }

        private WarnerApiConfiguration Config { get; }

        private HttpClient GetClient()
        {
            return new HttpClientFactory(Config).GetClient();
        }

        protected TResult QueryParse<TResult>(string apiAddress)
            where TResult : class
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    var awaitable = client.GetAsync(apiAddress);
                    awaitable.Wait();
                    return ParseResponse<TResult>(awaitable.Result);
                }
                catch (AggregateException ag)
                {
                    throw ag.InnerException;
                }
            }
        }

        protected TResultId Post<TResultId>(string url, string payload)
            where TResultId : struct
        {
            using (HttpClient client = GetClient())
            {
                HttpContent httpContent = new StringContent(
                    payload,
                    Encoding.UTF8,
                    "application/json");
                var awaitable = client.PostAsync(url, httpContent);
                try
                {
                    awaitable.Wait();
                    return ParseResponseValue<TResultId>(awaitable.Result);
                }
                catch (AggregateException ag)
                {
                    throw ag.InnerException;
                }
            }
        }

        protected void Post(string url, string payload)
        {
            using (HttpClient client = GetClient())
            {
                HttpContent httpContent = new StringContent(
                    payload,
                    Encoding.UTF8,
                    "application/json");
                var awaitable = client.PostAsync(url, httpContent);
                try
                {
                    awaitable.Wait();
                }
                catch (AggregateException ag)
                {
                    throw ag.InnerException;
                }
            }
        }

        private static TResult ParseResponse<TResult>(HttpResponseMessage response)
            where TResult : class
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TResult>(resultString);
            }
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return null;
            }
            throw new InvalidOperationException("Unexpected response from API.");
        }

        private static TValueResult ParseResponseValue<TValueResult>(
            HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TValueResult>(resultString);
            }
            throw new InvalidOperationException("Unexpected response from API.");
        }
    }
}
