using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SafeLink.Enumerations;

namespace SafeLinks.Models
{
    public class ShortenerService
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public ShortenerServiceType Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("reportLink")]
        public string ReportLink { get; set; }
    }
}
