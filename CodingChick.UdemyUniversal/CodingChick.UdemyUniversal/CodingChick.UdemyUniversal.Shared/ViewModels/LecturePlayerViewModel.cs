using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Navigation;
using Caliburn.Micro;
using CodingChick.UdemyUniversal.Core.Services;
using CodingChick.UdemyUniversal.CoreUI;
using Microsoft.PlayerFramework;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class LecturePlayerViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private LecturesListViewModel _parameter;

        public LecturePlayerViewModel()
        {
        }

        public LecturePlayerViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
        }

        public LecturesListViewModel Parameter
        {
            get { return _parameter; }
            set
            {
                _parameter = value;
            }
        }

        private TimeSpan _positionInLecture;

        public TimeSpan PositionInLecture
        {
            get { return _positionInLecture; }
            set
            {
                _positionInLecture = value;
                NotifyOfPropertyChange(() => PositionInLecture);
            }
        }

        public async void PostFinishLecture()
        {
            var lecture = Parameter.CurrentLecture;

            if (lecture != null)
            {
                var succeed = await _dataService.PostLectureProgress(lecture.Id, true);
            }
        }

        public async void PostStartedWatchingLecture()
        {
            var lecture = (LecturePlayItem)Parameter.CurrentPlaylistItem;
            var succeed = await _dataService.PostLectureProgress(lecture.Lecture.Id, false);
        }

        public void ReachedEndNavigateBack()
        {
            _navigationService.GoBack();
        }
    }
}
