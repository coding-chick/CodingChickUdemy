using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class PromoAsset
    {
        // public string __class { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("contextInfo")]
        public string ContextInfo { get; set; }
        [JsonProperty("streamUrl")]
        public string StreamUrl { get; set; }
        [JsonProperty("downloadUrl")]
        public DownloadUrl DownloadUrl { get; set; }
        [JsonProperty("data")]
        public Data Data { get; set; }
        [JsonProperty("captions")]
        public List<object> Captions { get; set; }
    }
}