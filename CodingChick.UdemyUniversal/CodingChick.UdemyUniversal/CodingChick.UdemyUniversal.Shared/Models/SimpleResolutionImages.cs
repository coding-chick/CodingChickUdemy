using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class SimpleResolutionImages
    {
        [JsonProperty("img_50x50")]
        public string Img50X50 { get; set; }
        [JsonProperty("img_75x75")]
        public string Img75X75 { get; set; }
        [JsonProperty("img_100x100")]
        public string Img100X100 { get; set; }
        [JsonProperty("img_125_H")]
        public string Img125H { get; set; }
        [JsonProperty("img_200_H")]
        public string Img200H { get; set; }
    }
}