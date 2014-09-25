using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CodingChick.UdemyUniversal.Models;
using CodingChick.UdemyUniversal.ViewModels;

namespace CodingChick.UdemyUniversal.Controls
{
    public class ChapterLectureTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Chapter { get; set; }

        public DataTemplate Lecture { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is LectureViewModel)
            {
                return Lecture;
            }

            return Chapter;
        }
    }
}
