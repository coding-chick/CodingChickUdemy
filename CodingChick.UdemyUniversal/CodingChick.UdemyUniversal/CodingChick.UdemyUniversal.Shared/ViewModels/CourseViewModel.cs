using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class CourseViewModel
    {
        public string Name { get; set; }
        public Uri ImageUri { get; set; }
        public string Price { get; set; }
        public string OriginalPrice { get; set; }
    }
}
