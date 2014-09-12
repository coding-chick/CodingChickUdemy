using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CodingChick.UdemyUniversal.Models;

namespace CodingChick.UdemyUniversal.Controls
{
    public class ChapterLectureTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Chapter { get; set; }

        public DataTemplate Lecture { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item.GetType() == typeof (Lecture))
            {
                return Lecture;
            }

            return Chapter;
        }
    }
}
