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

            if (uri == null)
            {
                return null;
            }

            return IsUrlShortenerDomain(uri)
                ? _source.GetLinkLocation(uri)
                : null;
        }

        private Uri ConvertToUri(string url)
        {
            Uri uri = null;

            if (!string.IsNullOrEmpty(url))
            {
                var decodedUrl = WebUtility.UrlDecode(url);
                var isUri = Uri.IsWellFormedUriString(decodedUrl, UriKind.Absolute);

                if (isUri)
                {
                    uri = new Uri(decodedUrl);
                }
            }

            return uri;
        }

        private bool IsUrlShortenerDomain(Uri uri)
        {
            var knownDomains = new string[]
            {
                "bit.ly",
                "tiny.cc"
            };

            return knownDomains.Any(x => x == uri.Host);
        }
    }
}