using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class User
    {
        //[JsonProperty("img_480x270")]
        //public string __class { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("images")]
        public SimpleResolutionImages Images { get; set; }
    }
}