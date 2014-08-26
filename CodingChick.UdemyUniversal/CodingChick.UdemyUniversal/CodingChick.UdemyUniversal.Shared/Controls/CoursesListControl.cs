using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace CodingChick.UdemyUniversal.Controls
{
    public class CoursesListControl : ListView
    {
        public CoursesListControl()
        {
            (this).DefaultStyleKey = typeof(CoursesListControl);            
        }
    }
}
