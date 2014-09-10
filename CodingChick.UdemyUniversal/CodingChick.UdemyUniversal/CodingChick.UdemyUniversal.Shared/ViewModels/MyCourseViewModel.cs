using System;
using System.Linq;
using CodingChick.UdemyUniversal.Models;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class MyCourseViewModel
    {
        public Course CourseModel { get; set; }
        public string CourseId { get; set; }
        public Uri ImageUri { get; set; }
    }
}