using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class CoursesViewModel : Screen
    {
        public CoursesViewModel()
        {
            DiscoveryCourses = new ObservableCollection<CourseViewModel>()
            {
                new CourseViewModel(){Image = new Image(), Name = "test1"},
                new CourseViewModel(){Image = new Image(), Name = "test2"}
            };
        }


        private ObservableCollection<CourseViewModel> _discoveryCourses;
        public ObservableCollection<CourseViewModel> DiscoveryCourses
        {
            get { return _discoveryCourses; }
            set
            {
                _discoveryCourses = value;
                NotifyOfPropertyChange(() => DiscoveryCourses);
            }
        }
    }
}