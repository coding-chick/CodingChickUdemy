using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class Dimensions
    {
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
    }
}