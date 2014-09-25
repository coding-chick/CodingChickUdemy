using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using CodingChick.UdemyUniversal.Core.Services;
using CodingChick.UdemyUniversal.CoreUI;
using CodingChick.UdemyUniversal.Models;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class CoursesListViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private CategoryViewModel _parameter;
        private PagedCollection<CourseViewModel> _coursesList;

        public CoursesListViewModel()
        {
            CoursesList = new PagedCollection<CourseViewModel>(null)
            {
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/114650_f58f_5.jpg"), Name = "course to learn something course to learn something course to learn something course to learn something", OriginalPrice = "50$", Price = "14.9$"},
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/185774_cb62.jpg"), Name = "course to learn something", OriginalPrice = "50$", Price = "14.9"},
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/140238_dd80_2.jpg"), Name = "course to learn something", OriginalPrice = "50$", Price = "14.9"},
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/140238_dd80_2.jpg"), Name = "course to learn something", OriginalPrice = "50$", Price = "14.9"},
            };

            Parameter = new CategoryViewModel() {Title = "Category Title"};
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            CoursesList = new PagedCollection<CourseViewModel>(LoadMoreCourses);
        }

        public CoursesListViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
        }

        public CategoryViewModel Parameter
        {
            get
            {
                return _parameter;
            }
            set
            {
                _parameter = value;
                CategoryTitle = Parameter.Title;
            }
        }

        public string CategoryTitle { get; set; }

        private async Task<IEnumerable<CourseViewModel>> LoadMoreCourses(uint count, int pageNumber)
        {
            var coursesInCategory = await _dataService.GetCoursesInCategory(Parameter.Id, 12, pageNumber);
            return coursesInCategory.CoursesList.Select(course => new CourseViewModel()
            {
                CourseModel = course,
                CourseId = course.Id,
                ImageUri = new Uri(course.Images.Img480X270),
                Price = course.InAppPurchasePriceText,
            });
        }

        public PagedCollection<CourseViewModel> CoursesList
        {
            get { return _coursesList; }
            set
            {
                _coursesList = value;
                NotifyOfPropertyChange(() => CoursesList);
            }
        }

        public void ShowCourseDetails(ItemClickEventArgs args)
        {
            Course courseModel;
            if (args.ClickedItem is MyCourseViewModel)
                courseModel = ((MyCourseViewModel)args.ClickedItem).CourseModel;
            else
                courseModel = ((CourseViewModel)args.ClickedItem).CourseModel;

            _navigationService.NavigateToViewModel<CourseDetailsViewModel>(courseModel);
        }
    }
}