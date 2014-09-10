using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class Data
    {
        public string Name { get; set; }
        public string TicketId { get; set; }
        public int IsHd { get; set; }
        public int JobId { get; set; }
        public int InputHeight { get; set; }
        public double Duration { get; set; }
        public Dimensions Dimensions { get; set; }
        public string ProcessedThumbFileName { get; set; }
        public string GetVideoPlayerThumbmailFrom { get; set; }
        public string CreationMethod { get; set; }
    }
}