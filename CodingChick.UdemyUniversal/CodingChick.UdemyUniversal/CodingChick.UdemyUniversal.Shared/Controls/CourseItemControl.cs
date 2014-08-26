using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace CodingChick.UdemyUniversal.Controls
{
    public class CourseItemControl : ContentControl
    {
        public CourseItemControl()
        {
            (this).DefaultStyleKey = typeof(CourseItemControl);
        }

        //public Brush PriceColorBrush { get; set; }

        public static readonly DependencyProperty PriceColorBrushProperty = DependencyProperty.Register(
            "PriceColorBrush", typeof(Brush), typeof(CourseItemControl), new PropertyMetadata(null));

        public Brush PriceColorBrush
        {
            get { return (Brush)GetValue(PriceColorBrushProperty); }
            set { SetValue(PriceColorBrushProperty, value); }
        }
    }
}
