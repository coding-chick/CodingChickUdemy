using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChick.UdemyUniversal.Models
{
    public class ReviewsListPage
    {
        public List<Review> Data { get; set; }
        public Pagination Pagination { get; set; }
    }
}
