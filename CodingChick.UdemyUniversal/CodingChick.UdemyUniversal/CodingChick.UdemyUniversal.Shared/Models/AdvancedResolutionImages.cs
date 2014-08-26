using System;
using System.Text;
using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class AdvancedResolutionImages : SimpleResolutionImages
    {
        [JsonProperty("img_480x270")]
        public string Img480X270 { get; set; }
        [JsonProperty("img_304x171")]
        public string Img304X171 { get; set; }
        [JsonProperty("img_240x135")]
        public string Img240X135 { get; set; }
        [JsonProperty("img_96x54")]
        public string Img96X54 { get; set; }
        [JsonProperty("img_48x27")]
        public string Img48X27 { get; set; }
    }
}
