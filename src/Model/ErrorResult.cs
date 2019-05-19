using Newtonsoft.Json;

namespace SafeLinks.Model
{
    public class ErrorResult
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}