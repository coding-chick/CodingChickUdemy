using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using CodingChick.UdemyUniversal.Core.Services;
using CodingChick.UdemyUniversal.CoreUI;
using CodingChick.UdemyUniversal.Models;
using System.Linq;
using Action = System.Action;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class CourseDetailsViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _iDataService;
        private readonly IEventAggregator _eventAggregator;
        private Course _parameter;
        private string _students;
        private ObservableCollection<CurciculumViewModelBase> _curiculum;
        private Visibility _purchaceButtonVisibility;

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

            Curiculum = new ObservableCollection<CurciculumViewModelBase>()
            {
                new ChapterViewModel() {Chapter = new Chapter(){ChapterIndex = 1, Title = "First chapter"}},
                new LectureViewModel() {Lecture = new Lecture(){ChapterIndex = 1, Title = "First lecture"}},
                new LectureViewModel() {Lecture = new Lecture(){ChapterIndex = 2, Title = "Second lecture"}},
                new ChapterViewModel() {Chapter = new Chapter(){ChapterIndex = 2, Title = "First lecture"}},

            };
        }

        public CourseDetailsViewModel(INavigationService navigationService, IDataService iDataService, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _iDataService = iDataService;
            _eventAggregator = eventAggregator;
        }

        public string Students
        {
            get { return _students; }
            set { _students = value + " students"; }
        }

        protected async override void OnActivate()
        {
            base.OnActivate();
            await CreateCuriculumViewModels();
        }

        private async Task CreateCuriculumViewModels()
        {
            var courseDetails = await _iDataService.GetFullCourseCuriculum(Parameter.Id);
            Curiculum = new ObservableCollection<CurciculumViewModelBase>();
            foreach (var chapter in courseDetails.Curriculum)
            {
                if (chapter.GetType() == typeof(Chapter))
                {
                    Curiculum.Add(new ChapterViewModel() { Chapter = chapter });
                }
                else
                {
                    var lectureViewModel = new LectureViewModel() { Lecture = chapter as Lecture };
                    if (IsCourseMine)
                        lectureViewModel.OwnedLecture = true;
                    else
                        lectureViewModel.OwnedLecture = false;

                    Curiculum.Add(lectureViewModel);
                }
            }
        }


        public ObservableCollection<CurciculumViewModelBase> Curiculum
        {
            get { return _curiculum; }
            set
            {
                _curiculum = value;
                NotifyOfPropertyChange(() => Curiculum);
            }
        }

        public Course Parameter
        {
            get { return _parameter; }
            set
            {
                _parameter = value;
                CourseImage = new Uri(_parameter.Images.Img480X270);
                Students = _parameter.NumSubscribers.ToString();

                NotifyOfPropertyChange(() => Parameter);
            }
        }

        public bool IsCourseMine
        {
            get { return Parameter.GetType() == typeof(MyCourse); }
        }

        public Uri CourseImage { get; set; }

        public Visibility PurchaceButtonVisibility
        {
            get
            {
                if (IsCourseMine)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

        public void PlayLecture(ItemClickEventArgs args)
        {
            var selectedLecture = (CurciculumViewModelBase)args.ClickedItem;
            if (selectedLecture.GetType() == typeof(LectureViewModel))
            {
                if (((LectureViewModel)selectedLecture).ShowPlayButton == Visibility.Visible)
                {
                    var lecturesListViewModel = CreateLecturesListViewModels(selectedLecture);
                    _navigationService.NavigateToViewModel<LecturePlayerViewModel>(lecturesListViewModel);
                }
                else
                {
                    UiServicesss.ShowCustomMessage(string.Format("Would you like to purchase this course for {0} on Udemy.com?", Parameter.Price), "You don't own this course", "Yes", "No", 
                        new UICommand("Purchase", command => PurchaceCourse()),
                        new UICommand("Cancel"));
                }
            }
        }

        private LecturesListViewModel CreateLecturesListViewModels(CurciculumViewModelBase selectedLecture)
        {
            var lecturesListViewModel = new LecturesListViewModel();
            lecturesListViewModel.Lectures = (from lecture in Curiculum
                where lecture.GetType() == typeof (LectureViewModel) &&
                      ((LectureViewModel) lecture).ShowPlayButton == Visibility.Visible
                orderby ((LectureViewModel) lecture).Lecture.ObjectIndex
                select ((LectureViewModel) lecture).Lecture).ToList();

            lecturesListViewModel.CurrentLecture = ((LectureViewModel) selectedLecture).Lecture;
            return lecturesListViewModel;
        }

        public string PurchaseButtonContent
        {
            get { return string.Format("Buy on Udemy.com for {0}", Parameter.Price); }
        }

        public void PurchaceCourse()
        {
            Launcher.LaunchUriAsync(new Uri(Parameter.Url));
        }

        public async void ReloadCourseDetails()
        {
            await UiServicesss.ShowProgressIndicatorForAction(async () =>
            {
                var myCourses = await _iDataService.GetMyCourses();
                var myCourse = myCourses.CoursesList.Select(c => c).Where(c => c.Id == Parameter.Id).ToList();

                if (myCourse.Any())
                {
                    Parameter = myCourse.FirstOrDefault();
                    await CreateCuriculumViewModels();
                }
            });
        }
    }
}
