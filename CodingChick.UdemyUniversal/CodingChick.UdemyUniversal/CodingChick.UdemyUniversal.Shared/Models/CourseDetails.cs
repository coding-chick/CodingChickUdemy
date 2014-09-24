using System.Collections.Generic;

namespace CodingChick.UdemyUniversal.Models
{
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