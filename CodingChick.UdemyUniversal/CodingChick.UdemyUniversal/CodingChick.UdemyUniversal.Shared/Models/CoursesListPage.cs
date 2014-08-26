using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class CoursesListPage
    {
        [JsonProperty("courses")]
        public List<Course> Courses { get; set; }
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}