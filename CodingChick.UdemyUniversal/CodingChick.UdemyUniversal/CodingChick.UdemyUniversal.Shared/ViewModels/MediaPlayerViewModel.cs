using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class MediaPlayerViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private Uri _parameter;

        public MediaPlayerViewModel()
        {
            Parameter = new Uri("https://dabm6vlh6a177.cloudfront.net/2014-07-22_07-33-18-7005d64399970264837e65dffa291bfa/WebHD_720p.mp4?response-content-disposition=attachment%3Bfilename%3Dsec-1-lec-1mp4.mp4&Expires=1409711814&Signature=qfKZvIsRJ6s3EUg5iYjJ-LgggxF~bVTsGZehETPkhUHPbUbo1lffvkxeKiQek3--mB4tagIhTmUCORDtx-VGpDAfbjr5rcrOZvC0X-Um3ya1B4y7Xhv8PX1AKDz2I8pShlIEJ5SFSGipyjY4PPpPiSfSdrdUDB187t4Kc5HXhq6zVG9-EwpVo-YxBCLsFhcCG2bn7uQtXHA6JGz9mCWDAeiJq5CUJ9j7hFTtHYaFSRYxl0H2mW3Ch6Q6xJ3ctcQTYbCHAbKDl3itPfFnJuzpZnz8mk4W7SOiZ9fqFKggkKQ7hkISc75RTShgxx6VOvvieevkmpJTjjJYVoGAk8LqLQ__&Key-Pair-Id=APKAI7LVCUGIDCBBVTCQ");
        }


        public MediaPlayerViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public Uri Parameter
        {
            get { return _parameter; }
            set { _parameter = value; }
        }
    }
}
