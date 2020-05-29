using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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

        public async Task<string> GetLinkLocationAsync(Uri uri)
        {
            var client = _clientFactory.CreateClient(httpClientName);
            var request = new HttpRequestMessage(HttpMethod.Head, uri);

            var response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.MovedPermanently)
            {
                return response.Headers.Location.ToString();
            }

            throw new WebException($"Status code was not 301 for uri {uri.OriginalString}");
        }
    }
}