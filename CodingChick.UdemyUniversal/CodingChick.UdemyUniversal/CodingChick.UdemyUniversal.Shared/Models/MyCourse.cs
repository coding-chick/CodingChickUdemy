using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class MyCourse : Course
    {
        public double AvgRatingRatio { get; set; }
        public string PublishedTime { get; set; }
        public object IsInstructor { get; set; }
        public bool CanEdit { get; set; }
        public string IsPremium { get; set; }
        public string GiftUrl { get; set; }
        public string IsPrivate { get; set; }
        [JsonProperty("__extras")]
        public Extras Extras { get; set; }
    }
}