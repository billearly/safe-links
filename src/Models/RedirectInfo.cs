using Newtonsoft.Json;

namespace SafeLinks.Models
{
    public class RedirectInfo
    {
        [JsonProperty("location")]
        public string Location { get; set; }
    }
}