using System;
using Windows.Globalization.DateTimeFormatting;

namespace CodingChick.UdemyUniversal.Models
{
    public class Review
    {
        private DateTime _created;
        public string Id { get; set; }
        public string CourseId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; }
        public string IsSpam { get; set; }

        public DateTime Created
        {
            get { return _created.Date; }
            set { _created = value; }
        }

        public string CreatedString
        {
            get
            {
                return
                    new DateTimeFormatter(YearFormat.Default, MonthFormat.Default, DayFormat.Default,
                        DayOfWeekFormat.None).Format(Created);
            }
        }

        public string CreatedInAgo { get; set; }
        public int RatingWidth { get; set; }
    }
}