using SafeLinks.Source;
using System;
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
            var decodedUrl = WebUtility.UrlDecode(url);
            var uri = new Uri(decodedUrl);

            return _source.GetLinkLocation(uri);
        }
    }
}