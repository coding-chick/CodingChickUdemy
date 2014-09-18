using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CodingChick.UdemyUniversal.CoreUI;

namespace CodingChick.UdemyUniversal.Controls
{
    [TemplateVisualState(GroupName = PlayLectureControl.LectureCompleteStatus, Name = PlayLectureControl.Notviewed)]
    [TemplateVisualState(GroupName = PlayLectureControl.LectureCompleteStatus, Name = PlayLectureControl.PartiallyViewed)]
    [TemplateVisualState(GroupName = PlayLectureControl.LectureCompleteStatus, Name = PlayLectureControl.Complete)]
    public class PlayLectureControl : Control
    {
        public const string LectureCompleteStatus = "LectureCompleteStates";

        public const string Notviewed = "NotViewed";
        public const string PartiallyViewed = "PartiallyViewed";
        public const string Complete = "Complete";

        public PlayLectureControl()
        {
            (this).DefaultStyleKey = typeof(PlayLectureControl);

        }

        public static readonly DependencyProperty PlayLectureControlStateProperty = DependencyProperty.Register(
            "PlayLectureControlState", typeof(PlayLectureControlState), typeof(PlayLectureControl), new PropertyMetadata(Controls.PlayLectureControlState.NotViewed, OnPlayLectureStateChanged));

        private static void OnPlayLectureStateChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = (PlayLectureControl)dependencyObject;
            var value = (PlayLectureControlState)dependencyPropertyChangedEventArgs.NewValue;
            if (control != null)
            {
                control.UpdateVisualState();
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateVisualState();
        }

        private void UpdateVisualState()
        {
            switch (this.PlayLectureControlState)
            {
                case PlayLectureControlState.NotViewed:
                    VisualStateManager.GoToState(this, PlayLectureControl.Notviewed, true);
                    break;
                case PlayLectureControlState.Viewed:
                    VisualStateManager.GoToState(this, PlayLectureControl.PartiallyViewed, true);
                    break;
                case PlayLectureControlState.Complete:
                    VisualStateManager.GoToState(this, PlayLectureControl.Complete, true);
                    break;
            }
        }

        public PlayLectureControlState PlayLectureControlState
        {
            get { return (PlayLectureControlState)GetValue(PlayLectureControlStateProperty); }
            set { SetValue(PlayLectureControlStateProperty, value); }
        }

    }

    public enum PlayLectureControlState
    {
        NotViewed,
        Viewed,
        Complete
    }
}
