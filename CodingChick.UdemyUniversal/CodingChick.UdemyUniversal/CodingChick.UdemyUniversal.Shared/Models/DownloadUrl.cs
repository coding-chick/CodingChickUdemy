using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class DownloadUrl
    {
        public List<string> Video { get; set; }
        public List<string> Video480p { get; set; }
        [JsonProperty("download")]
        public string Download { get; set; }
    }
}