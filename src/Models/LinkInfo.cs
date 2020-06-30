using System;
using System.Net;
using Newtonsoft.Json;
using SafeLink.Enumerations;

namespace SafeLinks.Models
{
    public class LinkInfo
    {
        private ShortenerService service;

        [JsonProperty("responseCode")]
        public int ResponseCode { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("linkOrigin")]
        public string LinkOrigin { get; set; }

        [JsonProperty("service")]
        public ShortenerService Service
        {
            get
            {
                if (service == null)
                {
                    service = GenerateShortenerService(LinkOrigin);
                };

                return service;
            }
        }

        private ShortenerService GenerateShortenerService(string url)
        {
            var decodedUrl = WebUtility.UrlDecode(url);
            var isUriString = Uri.IsWellFormedUriString(decodedUrl, UriKind.Absolute);

            if (!isUriString)
            {
                return null;
            }

            var uri = new Uri(decodedUrl);

            switch (uri.Host.ToLower())
            {
                // Ideally these definitions lives in config, or are persisted outside the app where they can be updated
                case "bit.ly":
                    return new ShortenerService
                    {
                        Type = ShortenerServiceType.Bitly,
                        Description = "This is a bitly link",
                        ReportLink = "https://support.bitly.com/hc/en-us/articles/231247908-I-ve-found-a-Bitly-link-that-directs-to-spam-what-should-I-do-"
                    };

                case "tinyurl.com":
                    return new ShortenerService
                    {
                        Type = ShortenerServiceType.TinyUrl,
                        Description = "This is a tinyurl link",
                        ReportLink = "https://tinyurl.com/"
                    };

                default:
                    return null;
            }
        }
    }
}
