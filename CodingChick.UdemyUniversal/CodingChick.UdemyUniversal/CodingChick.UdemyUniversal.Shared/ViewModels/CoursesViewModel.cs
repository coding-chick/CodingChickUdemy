using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Caliburn.Micro;
using CodingChick.UdemyUniversal.Core.Services;
using CodingChick.UdemyUniversal.CoreUI;
using CodingChick.UdemyUniversal.Models;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class CoursesViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _iDataService;

        public CoursesViewModel()
        {
            if (DesignMode.DesignModeEnabled)
            {
                OnSaleCourses = new PagedCollection<CourseViewModel>(null)
                {
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/114650_f58f_5.jpg"), Name = "course to learn something course to learn something course to learn something course to learn something", OriginalPrice = "50$", Price = "14.9$"},
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/185774_cb62.jpg"), Name = "course to learn something", OriginalPrice = "50$", Price = "14.9"},
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/140238_dd80_2.jpg"), Name = "course to learn something", OriginalPrice = "50$", Price = "14.9"},
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/140238_dd80_2.jpg"), Name = "course to learn something", OriginalPrice = "50$", Price = "14.9"},
                };
                NewCourses = new PagedCollection<CourseViewModel>(null)
                {
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/114650_f58f_5.jpg"), Name = "course to learn something course to learn something course to learn something course to learn something", OriginalPrice = "50$", Price = "14.9$"},
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/185774_cb62.jpg"), Name = "course to learn something", OriginalPrice = "50$", Price = "14.9"},
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/140238_dd80_2.jpg"), Name = "course to learn something", OriginalPrice = "50$", Price = "14.9"},
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/140238_dd80_2.jpg"), Name = "course to learn something", OriginalPrice = "50$", Price = "14.9"},
                };
            }
        }

        public CoursesViewModel(INavigationService navigationService, IDataService iDataService)
        {
            _navigationService = navigationService;
            _iDataService = iDataService;

        }

        protected override void OnActivate()
        {
            base.OnActivate();
            IntializeCoursesData();
        }

        private async void IntializeCoursesData()
        {
            OnSaleCourses = new PagedCollection<CourseViewModel>(LoadMoreOnSaleCourses);
            NewCourses = new PagedCollection<CourseViewModel>(LoadMoreNewCourses);

            Categories = await _iDataService.GetCategories();
        }

        private async Task<IEnumerable<CourseViewModel>> LoadMoreOnSaleCourses(uint count, int pageNumber)
        {
            var onSaleCourses = await _iDataService.GetCoursesOnSaleBasic(12, pageNumber);
            return onSaleCourses.Courses.Select(course => new CourseViewModel()
            {
                ImageUri = new Uri(course.Images.Img480X270),
                Name = course.Title,
                Price = course.InAppPurchasePriceText,
                OriginalPrice = course.OriginalPriceText,
            });
        }

        private async Task<IEnumerable<CourseViewModel>> LoadMoreNewCourses(uint u, int pageNumber)
        {
            var newCourses = await _iDataService.GetCoursesNewBasic(12, pageNumber);
            return newCourses.Courses.Select(course => new CourseViewModel()
            {
                ImageUri = new Uri(course.Images.Img480X270),
                Name = course.Title,
                Price = course.OriginalPriceText,
            });
        }


        private PagedCollection<CourseViewModel> _newCourses;
        public PagedCollection<CourseViewModel> NewCourses
        {
            get { return _newCourses; }
            set
            {
                _newCourses = value;
                NotifyOfPropertyChange(() => NewCourses);
            }
        }

        private PagedCollection<CourseViewModel> _onSaleCourses;
        public PagedCollection<CourseViewModel> OnSaleCourses
        {
            get { return _onSaleCourses; }
            set
            {
                _onSaleCourses = value;
                NotifyOfPropertyChange(() => OnSaleCourses);
            }
        }


        private List<Category> _categories;
        public List<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                NotifyOfPropertyChange(() => OnSaleCourses);
            }
        }
    }
}