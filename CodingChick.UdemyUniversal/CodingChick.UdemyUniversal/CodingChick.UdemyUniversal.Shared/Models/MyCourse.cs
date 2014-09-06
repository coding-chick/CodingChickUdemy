using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class MyCourse : Course
    {
        [JsonProperty("avgRatingRatio")]
        public int AvgRatingRatio { get; set; }
        [JsonProperty("publishedTime")]
        public string PublishedTime { get; set; }
        [JsonProperty("isInstructor")]
        public object IsInstructor { get; set; }
        [JsonProperty("canEdit")]
        public bool CanEdit { get; set; }
        [JsonProperty("isPremium")]
        public string IsPremium { get; set; }
        [JsonProperty("giftUrl")]
        public string GiftUrl { get; set; }
        [JsonProperty("isPrivate")]
        public string IsPrivate { get; set; }
        [JsonProperty("__extras")]
        public Extras Extras { get; set; }
    }
}