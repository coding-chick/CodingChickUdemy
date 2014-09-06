using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class Extras
    {
        [JsonProperty("completionRatio")]
        public int CompletionRatio { get; set; }
        [JsonProperty("nextLecture")]
        public object NextLecture { get; set; }
    }
}