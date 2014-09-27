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

                Categories = new ObservableCollection<CategoryViewModel>()
                {
                    new CategoryViewModel() {Id = "1", Title = "Humanities"},
                    new CategoryViewModel() {Id = "2", Title = "Math & Science"},
                    new CategoryViewModel() {Id = "3", Title = "Language"},
                    new CategoryViewModel() {Id = "4", Title = "Unrecognized"},
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
#if WINDOWS_PHONE_APP
            OnSaleCourses = new PagedCollection<CourseViewModel>(LoadMoreOnSaleCourses);
            NewCourses = new PagedCollection<CourseViewModel>(LoadMoreNewCourses);
#endif
#if WINDOWS_APP
            OnSaleCourses = new PagedCollection<CourseViewModel>(LoadMoreOnSaleCourses, 1);
            NewCourses = new PagedCollection<CourseViewModel>(LoadMoreNewCourses, 1);
#endif

            var categories = await _iDataService.GetCategories();
            Categories = new ObservableCollection<CategoryViewModel>(categories.Select(category => new CategoryViewModel()
            {

                Id = category.Id,
                Title = category.Title
            }));

            MyCourses = new PagedCollection<MyCourseViewModel>(LoadMoreMyCourses, 1);
        }

        //TODO: mycourses api don't really support paging so something needs to be done in an app level
        private async Task<IEnumerable<MyCourseViewModel>> LoadMoreMyCourses(uint count, int pageNumber)
        {
            var myCourses = await _iDataService.GetMyCourses();
            if (myCourses != null)
                return myCourses.CoursesList.Select(course => new MyCourseViewModel()
                {
                    CourseModel = course,
                    CourseId = course.Id,
                    ImageUri = new Uri(course.Images.Img480X270),
                });
            return new List<MyCourseViewModel>();
        }

        private async Task<IEnumerable<CourseViewModel>> LoadMoreOnSaleCourses(uint count, int pageNumber)
        {
            var onSaleCourses = await _iDataService.GetCoursesOnSaleFull(12, pageNumber);
            if (onSaleCourses != null)
                return onSaleCourses.CoursesList.Select(course => new CourseViewModel()
                {
                    CourseModel = course,
                    CourseId = course.Id,
                    ImageUri = new Uri(course.Images.Img480X270),
                    Price = course.InAppPurchasePriceText,
                });
            return new List<CourseViewModel>();
        }

        private async Task<IEnumerable<CourseViewModel>> LoadMoreNewCourses(uint u, int pageNumber)
        {
            var newCourses = await _iDataService.GetCoursesNewFull(12, pageNumber);
            if (newCourses != null)
                return newCourses.CoursesList.Select(course => new CourseViewModel()
                {
                    CourseModel = course,
                    CourseId = course.Id,
                    ImageUri = new Uri(course.Images.Img480X270),
                    Price = course.OriginalPriceText,
                });
            return new List<CourseViewModel>();
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

        private PagedCollection<MyCourseViewModel> _myCourses;
        public PagedCollection<MyCourseViewModel> MyCourses
        {
            get { return _myCourses; }
            set
            {
                _myCourses = value;
                NotifyOfPropertyChange(() => MyCourses);
            }
        }

        public Visibility MyCoursesVisbility
        {
            get
            {
                Visibility visibility;
                if (MyCourses != null && MyCourses.Count == 0)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;

            }
        }

        private ObservableCollection<CategoryViewModel> _categories;
        private Visibility _myCoursesVisbility;

        public ObservableCollection<CategoryViewModel> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                NotifyOfPropertyChange(() => Categories);
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

        public void ShowCategoryDetails(ItemClickEventArgs args)
        {
            var category = (CategoryViewModel)args.ClickedItem;
            _navigationService.NavigateToViewModel<CoursesListViewModel>(category);
        }

        public void NavigateToAbout()
        {
            _navigationService.NavigateToViewModel<AboutViewModel>();
        }
    }
}