using Newtonsoft.Json;

namespace SafeLinks.Models
{
    public class ErrorResult
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}