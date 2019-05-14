using System;
using System.Net;
using System.Net.Http;

namespace SafeLinks.Source
{
    public class LinkSource : ILinksSource
    {
        private readonly IHttpClientFactory _clientFactory;

        public LinkSource(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public string GetLinkLocation(Uri url)
        {
            var client = _clientFactory.CreateClient("customer-handler");
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = client.SendAsync(request);
            response.Wait();

            if (response.Result.StatusCode == HttpStatusCode.MovedPermanently)
            {
                return response.Result.Headers.Location.ToString();
            }

            throw new WebException($"Status code was not 301 for uri {url}");
        }
    }
}