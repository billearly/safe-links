using SafeLinks.Models;
using SafeLinks.Source;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SafeLinks.Managers
{
    public class LinkManager : ILinkManager
    {
        private readonly ILinkSource source;

        public LinkManager(ILinkSource source)
        {
            this.source = source;
        }

        public async Task<LinkInfo> GetLinkInfoAsync(string url)
        {
            var uri = ConvertToUri(url);
            var response = await source.GetLinkInfoAsync(uri);

            return new LinkInfo
            {
                ResponseCode = (int)response.StatusCode,
                Location = response.Headers.Location?.ToString() ?? string.Empty,
                LinkOrigin = uri.OriginalString
            };
        }

        private Uri ConvertToUri(string url)
        {
            var decodedUrl = WebUtility.UrlDecode(url);

            decodedUrl = EnsureProtocolExists(decodedUrl);

            var isUriString = Uri.IsWellFormedUriString(decodedUrl, UriKind.Absolute);

            if (!isUriString)
            {
                throw new ArgumentException($"'{url}' is not a valid uri");
            }

            return new Uri(decodedUrl);
        }

        private string EnsureProtocolExists(string url)
        {
            return url.StartsWith("http://") || url.StartsWith("https://")
                ? url
                : $"http://{url}";
        }
    }
}