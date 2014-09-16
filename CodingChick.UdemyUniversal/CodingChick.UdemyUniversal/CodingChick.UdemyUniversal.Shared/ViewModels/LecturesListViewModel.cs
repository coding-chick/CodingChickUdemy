using System.Collections.Generic;
using CodingChick.UdemyUniversal.Models;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class LecturesListViewModel
    {
        public LecturesListViewModel()
        {
            
        }
        public int LectureLocation { get; set; }

        private List<Lecture> _lectures;
        private Lecture _currentLecture;

        public Lecture CurrentLecture
        {
            get { return _currentLecture; }
            set
            {
                _currentLecture = value;
                if (Lectures != null)
                    LectureLocation = Lectures.FindIndex(lecture => lecture.Id == CurrentLecture.Id);
            }
        }

        public List<Lecture> Lectures
        {
            get { return _lectures; }
            set { _lectures = value; }
        }

        public void GetNextLecture()
        {
            ++LectureLocation;
            CurrentLecture = Lectures.Count <= LectureLocation ? Lectures[LectureLocation] : null;
        }
    }
}