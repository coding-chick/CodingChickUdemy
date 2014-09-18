using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Caliburn.Micro;
using CodingChick.UdemyUniversal.Controls;
using CodingChick.UdemyUniversal.Models;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class LectureViewModel : CurciculumViewModelBase
    {
        public Lecture Lecture { get; set; }

        public PlayLectureControlState PlayLectureState
        {
            get
            {
                if (Lecture.CompletionRatio.HasValue && Lecture.CompletionRatio.Value == 100)
                    return PlayLectureControlState.Complete;
                if (Lecture.CompletionRatio.HasValue && Lecture.CompletionRatio.Value == 50)
                    return PlayLectureControlState.Viewed;
                return PlayLectureControlState.NotViewed;
            }
        }

        public bool OwnedLecture { get; set; }

        public Visibility ShowPlayButton
        {
            get
            {
                if ((!OwnedLecture && Lecture.IsFree == "Yes") || OwnedLecture)
                    return Visibility.Visible;
                return Visibility.Collapsed;
            } 
        }
    }

    public class ChapterViewModel :CurciculumViewModelBase
    {
        public Chapter Chapter { get; set; }
    }

    public class CurciculumViewModelBase : PropertyChangedBase 
    {
    }

}
