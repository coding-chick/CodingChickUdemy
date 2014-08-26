using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Caliburn.Micro;
using CodingChick.UdemyUniversal.Core.Services;
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
                OnSaleCourses = new List<CourseViewModel>()
                {
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/114650_f58f_5.jpg"), Name = "course to learn something course to learn something course to learn something course to learn something", OriginalPrice = "50$", Price = "14.99$"},
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/185774_cb62.jpg"), Name = "course to learn something", OriginalPrice = "50$", Price = "14.99$"},
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/140238_dd80_2.jpg"), Name = "course to learn something", OriginalPrice = "50$", Price = "14.99$"},
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/140238_dd80_2.jpg"), Name = "course to learn something", OriginalPrice = "50$", Price = "14.99$"},
                };


                NewCourses = new List<CourseViewModel>()
                {
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/114650_f58f_5.jpg"), Name = "course to learn something course to learn something course to learn something course to learn something", OriginalPrice = "50$", Price = "14.99$"},
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/185774_cb62.jpg"), Name = "course to learn something", OriginalPrice = "50$", Price = "14.99$"},
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/140238_dd80_2.jpg"), Name = "course to learn something", OriginalPrice = "50$", Price = "14.99$"},
                    new CourseViewModel() {ImageUri = new Uri("https://udemyimages-a.akamaihd.net/course/480x270/140238_dd80_2.jpg"), Name = "course to learn something", OriginalPrice = "50$", Price = "14.99$"},
                };
            }
        }

        public CoursesViewModel(INavigationService navigationService, IDataService iDataService)
        {
            _navigationService = navigationService;
            _iDataService = iDataService;

            IntializeCoursesData();
        }

        private async void IntializeCoursesData()
        {
            var onSaleCourses = await _iDataService.GetCoursesOnSaleBasic();

            OnSaleCourses = new List<CourseViewModel>(onSaleCourses.Courses.Select(course => new CourseViewModel()
            {
                ImageUri = new Uri(course.Images.Img480X270),
                Name = course.Title,
                Price = course.InAppPurchasePriceText,
                OriginalPrice = course.OriginalPriceText,
            }));

            //TODO: implement the paging mechanism
            var newCourses = await _iDataService.GetCoursesNewBasic(12, 1);

            NewCourses = new List<CourseViewModel>(newCourses.Courses.Select(course => new CourseViewModel()
            {
                ImageUri = new Uri(course.Images.Img480X270),
                Name = course.Title,
                Price = course.OriginalPriceText,
            }));

            Categories = await _iDataService.GetCategories();
        }


        private List<CourseViewModel> _newCourses;
        public List<CourseViewModel> NewCourses
        {
            get { return _newCourses; }
            set
            {
                _newCourses = value;
                NotifyOfPropertyChange(() => NewCourses);
            }
        }

        private List<CourseViewModel> _onSaleCourses;
        public List<CourseViewModel> OnSaleCourses
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