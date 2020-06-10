using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SafeLinks.Source
{
    public class LinkSource : ILinkSource
    {
        private const string httpClientName = "no-follow-redirect";

        private readonly IHttpClientFactory clientFactory;

        public LinkSource(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<HttpResponseMessage> GetLinkInfoAsync(Uri uri)
        {
            var client = clientFactory.CreateClient(httpClientName);
            var request = new HttpRequestMessage(HttpMethod.Head, uri);

            return await client.SendAsync(request);
        }
    }
}