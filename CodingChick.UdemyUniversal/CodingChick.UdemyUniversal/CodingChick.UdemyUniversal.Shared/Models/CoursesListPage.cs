using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class CoursesListPage<T> where T : Course
    {
        [JsonProperty("courses")]
        public List<T> CoursesList { get; set; }
        public Pagination Pagination { get; set; }
    }
}