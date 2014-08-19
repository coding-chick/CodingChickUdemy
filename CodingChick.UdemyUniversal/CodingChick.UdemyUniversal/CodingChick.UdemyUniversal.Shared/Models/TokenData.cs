using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class TokenData
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }
    }
}