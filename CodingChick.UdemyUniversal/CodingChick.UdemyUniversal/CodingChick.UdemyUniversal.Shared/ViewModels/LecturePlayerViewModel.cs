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
            _navigationService.Navigating += _navigationService_Navigating;
        }

        void _navigationService_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {

            }
        }

        public LecturesListViewModel Parameter
        {
            get { return _parameter; }
            set
            {
                _parameter = value;
                if (_parameter != null)
                {
                    LecturesPlaylist = new ObservableCollection<PlaylistItem>(_parameter.Lectures.Select(
                            lecture => new LecturePlayItem()
                            {
                                SourceUri = lecture.Asset.StreamUrl,
                                Source = new Uri(lecture.Asset.StreamUrl),
                                Lecture = lecture
                            }));
                    
                    CurrentLecture = LecturesPlaylist[_parameter.LectureLocation];
                }
            }
        }

        public ObservableCollection<PlaylistItem> LecturesPlaylist
        {
            get { return _lecturesPlaylist; }
            set
            {
                _lecturesPlaylist = value;
                NotifyOfPropertyChange(() => LecturesPlaylist);
                
            }
        }

        private TimeSpan _positionInLecture;
        private PlaylistItem _currentLecture;
        private ObservableCollection<PlaylistItem> _lecturesPlaylist;

        public TimeSpan PositionInLecture    
        {
            get { return _positionInLecture; }
            set
            {
                _positionInLecture = value;
                NotifyOfPropertyChange(() => PositionInLecture);
            }
        }


        public async void PostLecturePosition(object sender, EventArgs args)
        {
            var lecture = (LecturePlayItem) CurrentLecture;
            TimeSpan secondsInLecture = TimeSpan.Parse(lecture.Lecture.ContextInfo);
            var roundedPositionInLecture = new TimeSpan(((long)Math.Round((1.0 * PositionInLecture.Ticks / 10000000)) * 10000000));
            if (roundedPositionInLecture < secondsInLecture)
            {
                var succeed = await _dataService.PostLectureProgress(lecture.Lecture.Id, false);
            }
            else
            {
                var succeed = await _dataService.PostLectureProgress(lecture.Lecture.Id, true);
            }
        }

        public PlaylistItem CurrentLecture
        {
            get { return _currentLecture; }
            set
            {
                _currentLecture = value;
                NotifyOfPropertyChange(() => CurrentLecture);
            }
        }
    }
}
