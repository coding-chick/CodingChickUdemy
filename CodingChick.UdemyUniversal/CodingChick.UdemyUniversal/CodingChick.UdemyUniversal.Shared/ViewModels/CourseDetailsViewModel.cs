using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using CodingChick.UdemyUniversal.Core.Services;
using CodingChick.UdemyUniversal.Models;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class CourseDetailsViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _iDataService;
        private Course _parameter;
        private string _students;

        public CourseDetailsViewModel()
        {
            Parameter = new Course()
            {
                Title = "Workshop in probability and Statistics",
                Headline = "This workshop will teach you the fundamentals of statistics in order to give you a leg up at work or in school.",
                AvgRating = 3.5,
                NumSubscribers = 2534,
                Description = "This workshop is designed to help you make sense of basic probability and statistics with easy-to-understand explanations of all the subject\u2019s most important concepts. If you are in a statistics class and struggling with your assigned textbook or lecture material, this workshop was built with you in mind.",
                IsInAppPurchaseEnabled = true,
                Images = new AdvancedResolutionImages() { Img480X270 = "https://dujk9xa5fr1wz.cloudfront.net/course/480x270/185774_cb62_2.jpg" },
            };

            Students = Parameter.NumSubscribers.ToString();
        //    CourseImage = new Uri("https://dujk9xa5fr1wz.cloudfront.net/course/480x270/185774_cb62_2.jpg");
        }

        public CourseDetailsViewModel(INavigationService navigationService, IDataService iDataService)
        {
            _navigationService = navigationService;
            _iDataService = iDataService;
        }

        public string Students
        {
            get { return _students; }
            set { _students = value + " students"; }
        }

        protected async override void OnActivate()
        {
            base.OnActivate();
            var courseDetails = await _iDataService.GetCourseCuriculum(Parameter.Id);

        }

        public Course Parameter
        {
            get { return _parameter; }
            set
            {
                _parameter = value;
                CourseImage = new Uri(_parameter.Images.Img480X270);
                Students = _parameter.NumSubscribers.ToString();
            }
        }

        public Uri CourseImage { get; set; }
    }
}
