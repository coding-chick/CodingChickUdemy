namespace CodingChick.UdemyUniversal.Models
{
    public class Lecture : Chapter
    {
        public string Description { get; set; }
        public string AssetType { get; set; }
        public int? LectureIndex { get; set; }
        public string ContextInfo { get; set; }
        public string Url { get; set; }
        public string IsFree { get; set; }
        public bool? DiscussionExists { get; set; }
        public bool? SourceCodeExists { get; set; }
        public bool? ExternalLinkExists { get; set; }
        public string IconClass { get; set; }
        public bool? NoteExists { get; set; }
        public Asset Asset { get; set; }
    }
}