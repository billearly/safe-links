using System;
using System.Net;
using System.Net.Http;

namespace SafeLinks.Source
{
    public class LinkSource : ILinkSource
    {
        private const string httpClientName = "no-follow-redirect";

        private readonly IHttpClientFactory _clientFactory;

        public LinkSource(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public string GetLinkLocation(Uri uri)
        {
            var client = _clientFactory.CreateClient(httpClientName);
            var request = new HttpRequestMessage(HttpMethod.Head, uri);

            var response = client.SendAsync(request);
            response.Wait();

            if (response.Result.StatusCode == HttpStatusCode.MovedPermanently)
            {
                return response.Result.Headers.Location.ToString();
            }

            throw new WebException($"Status code was not 301 for uri {uri.OriginalString}");
        }
    }
}