using SafeLinks.Models;
using SafeLinks.Source;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SafeLinks.Managers
{
    public class LinkManager : ILinkManager
    {
        private readonly ILinkSource _source;

        public LinkManager(ILinkSource source)
        {
            _source = source;
        }

        public async Task<RedirectInfo> GetLinkLocationAsync(string url)
        {
            var uri = ConvertToUri(url);
            var linkLocation = await _source.GetLinkLocationAsync(uri);

            return new RedirectInfo
            {
                Location = linkLocation
            };
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
    }
}