namespace CodingChick.UdemyUniversal.Models
{
    public class Asset
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ContextInfo { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Status { get; set; }
        public int RemainingProcessingTime { get; set; }
        public string StreamUrl { get; set; }
        public DownloadUrl DownloadUrl { get; set; }
        public Data Data { get; set; }
    }
}