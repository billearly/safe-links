using SafeLinks.Source;
using System;
using System.Linq;
using System.Net;

namespace SafeLinks.Managers
{
    public class LinkManager : ILinkManager
    {
        private readonly ILinksSource _source;

        public LinkManager(ILinksSource source)
        {
            _source = source;
        }

        public string GetLinkLocation(string url)
        {
            var uri = ConvertToUri(url);

            if (!IsUrlShortenerDomain(uri))
            {
                throw new ArgumentException($"'{uri.Host}' is not a known url shortener domain");
            }

            return _source.GetLinkLocation(uri);
        }

        private Uri ConvertToUri(string url)
        {
            var decodedUrl = WebUtility.UrlDecode(url);
            var isUri = Uri.IsWellFormedUriString(decodedUrl, UriKind.Absolute);

            if (!isUri)
            {
                throw new ArgumentException($"'{url}' is not a valid uri");
            }

            return new Uri(decodedUrl);
        }

        private bool IsUrlShortenerDomain(Uri uri)
        {
            // This list could come from config
            var urlShortenerDomains = new string[]
            {
                "bit.ly",
                "tiny.cc"
            };

            return urlShortenerDomains.Any(x => x == uri.Host);
        }
    }
}