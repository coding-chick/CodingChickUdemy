using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using CodingChick.UdemyUniversal.Models;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class CourseViewModel
    {
        public Course CourseModel { get; set; }

        public string CourseId { get; set; }

        private string _price;
        public string Name { get; set; }
        public Uri ImageUri { get; set; }

        public string Price
        {
            get { return _price; }
            set
            {
                string price = value;
                if (string.IsNullOrEmpty(price))
                {
                    price = "0";
                }

                if (price[0] == '$')
                {
                    price = price.Remove(0, 1);
                }
                if (price.Count() > 4)
                {
                    price = price.Remove(4);
                }

                price = price + '$';
                _price = price;
            }
        }

        public string OriginalPrice { get; set; }
    }
}
