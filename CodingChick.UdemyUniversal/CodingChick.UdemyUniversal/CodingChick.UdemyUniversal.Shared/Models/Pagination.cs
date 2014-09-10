using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class Pagination
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int Offset { get; set; }
    }
}