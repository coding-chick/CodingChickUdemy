using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class Pagination
    {
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("offset")]
        public int Offset { get; set; }
    }
}