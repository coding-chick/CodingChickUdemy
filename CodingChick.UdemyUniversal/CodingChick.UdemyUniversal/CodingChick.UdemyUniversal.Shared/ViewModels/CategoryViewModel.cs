using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Caliburn.Micro;
using CodingChick.UdemyUniversal.CoreUI;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class CategoryViewModel    
    {
        public string Id { get; set; }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
            }
        }

        private string _title;

        public Uri CategoryIconUri
        {
            get
            {
                string iconPath;
                if (StringConsts.CategoriesUrls.TryGetValue(Title, out iconPath))
                {
                    return new Uri(iconPath, UriKind.Absolute);
                }
                return new Uri(StringConsts.CategoriesUrls["Default"], UriKind.Absolute);
            }
        }
    }
}