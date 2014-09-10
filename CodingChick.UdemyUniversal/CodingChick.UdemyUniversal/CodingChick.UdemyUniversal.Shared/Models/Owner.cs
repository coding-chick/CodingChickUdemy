using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class User
    {
        //[JsonProperty("img_480x270")]
        //public string __class { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public SimpleResolutionImages Images { get; set; }
    }
}