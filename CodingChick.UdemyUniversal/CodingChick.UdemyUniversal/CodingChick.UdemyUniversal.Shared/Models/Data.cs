using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class Data
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("ticketId")]
        public string TicketId { get; set; }
        [JsonProperty("isHD")]
        public int IsHd { get; set; }
        [JsonProperty("jobId")]
        public int JobId { get; set; }
        [JsonProperty("inputHeight")]
        public int InputHeight { get; set; }
        [JsonProperty("duration")]
        public double Duration { get; set; }
        [JsonProperty("dimensions")]
        public Dimensions Dimensions { get; set; }
        [JsonProperty("processedThumbFileName")]
        public string ProcessedThumbFileName { get; set; }
        [JsonProperty("getVideoPlayerThumbmailFrom")]
        public string GetVideoPlayerThumbmailFrom { get; set; }
        [JsonProperty("creationMethod")]
        public string CreationMethod { get; set; }
    }
}