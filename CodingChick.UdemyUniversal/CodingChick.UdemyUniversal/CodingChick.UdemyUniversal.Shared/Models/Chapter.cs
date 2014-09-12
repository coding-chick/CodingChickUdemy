using System.Collections.Generic;

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
 
    public class Faq
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    public class WhatYouWillLearnData
    {
        public List<string> Items { get; set; }
    }

    public class RequirementsData
    {
        public List<string> Items { get; set; }
    }

    public class CourseDetails
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public List<Chapter> Curriculum { get; set; }
        public List<Faq> Faq { get; set; }
        public WhatYouWillLearnData WhatYouWillLearnData { get; set; }
        public RequirementsData RequirementsData { get; set; }
    }
}