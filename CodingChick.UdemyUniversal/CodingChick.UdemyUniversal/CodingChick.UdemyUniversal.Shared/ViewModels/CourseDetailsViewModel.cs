using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using CodingChick.UdemyUniversal.Core.Services;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class CourseDetailsViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _iDataService;
        private string _parameter;

        public CourseDetailsViewModel(INavigationService navigationService, IDataService iDataService)
        {
            _navigationService = navigationService;
            _iDataService = iDataService;
        }

        protected async override void OnActivate()
        {
            base.OnActivate();
            //var courseDetails = _iDataService.GetFullCourseDetails();
        }

        public string Parameter
        {
            get { return _parameter; }
            set { _parameter = value; }
        }
    }
}
