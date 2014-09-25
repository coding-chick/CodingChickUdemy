using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Caliburn.Micro;
using CodingChick.UdemyUniversal.CoreUI;
using CodingChick.UdemyUniversal.Models;
using Microsoft.PlayerFramework;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class LecturesListViewModel : PropertyChangedBase
    {
        public LecturesListViewModel()
        {

        }
        public int LectureLocation { get; set; }

        private List<Lecture> _lectures;
        private Lecture _currentLecture;
        private PlaylistItem _currentPlaylistItem;
        private PlaylistItem _newPlaylistItem;

        public Lecture CurrentLecture
        {
            get { return _currentLecture; }
            set
            {
                _currentLecture = value;
                if (Lectures != null)
                {
                    LectureLocation = Lectures.FindIndex(lecture => lecture.Id == CurrentLecture.Id);
                    CurrentPlaylistItem = LecturesPlaylist[LectureLocation];
                }

            }
        }

        public List<Lecture> Lectures
        {
            get { return _lectures; }
            set
            {
                _lectures = value;
                LecturesPlaylist = new ObservableCollection<PlaylistItem>(_lectures.Select(
                          lecture => new LecturePlayItem()
                          {
                              SourceUri = string.IsNullOrEmpty(lecture.Asset.StreamUrl) ? null : lecture.Asset.StreamUrl,
                              Lecture = lecture
                          }));
            }
        }

        public void GetNextLecture()
        {
            ++LectureLocation;
            if (LectureLocation >= Lectures.Count)
                ReachedEndOfList = true;
            else
                ReachedEndOfList = false;

            if (!ReachedEndOfList)
            {
                CurrentLecture = Lectures[LectureLocation];
                //CurrentPlaylistItem = LecturesPlaylist[LectureLocation];
            }
        }

        public bool ReachedEndOfList { get; set; }

        public ObservableCollection<PlaylistItem> LecturesPlaylist { get; set; }

        public PlaylistItem CurrentPlaylistItem
        {
            get { return _currentPlaylistItem; }
            set
            {
                _currentPlaylistItem = value;
                CheckIfCanPlayItem();
                NotifyOfPropertyChange(() => CurrentPlaylistItem);
            }
        }

        public PlaylistItem NewPlaylistItem
        {
            get { return _newPlaylistItem; }
            set
            {
                _newPlaylistItem = value;
                GetNextLecture();
            }
        }

        private async void CheckIfCanPlayItem()
        {
            if (string.IsNullOrEmpty(CurrentLecture.Asset.StreamUrl))
            {
                await
                    UiServices.ShowCustomMessage("Media isn't playeable at this time, moving to the next lecture", "Sorry", "Ok",
                        string.Empty, new UICommand("Ok"), null);
                GetNextLecture();
            }
        }
    }
}