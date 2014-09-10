namespace CodingChick.UdemyUniversal.Models
{
    public class Chapter
    {
        public string Id { get; set; }
        public string CourseId { get; set; }
        public string Type { get; set; }
        public string TypeText { get; set; }
        public int SortOrder { get; set; }
        public int ChapterIndex { get; set; }
        public int ObjectIndex { get; set; }
        public string IsPublished { get; set; }
        public string Title { get; set; }
        public int Index { get; set; }
        public bool IsLocked { get; set; }
    }
}