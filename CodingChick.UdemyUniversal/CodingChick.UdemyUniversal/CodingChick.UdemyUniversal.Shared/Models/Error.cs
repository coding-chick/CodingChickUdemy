using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class Error
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("details")]
        public string Details { get; set; }
    }
}