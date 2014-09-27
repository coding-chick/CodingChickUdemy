using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class AboutViewModel
    {
        public AboutViewModel()
        {
            Title = "CodingChick's Udemy";
            Email = @"mailto:efratb@codingchick.net";
            PrivcayPolicyLink = @"http://codingchick.net/Privacy/PrivacyPolicy.txt";
            EmailText = "efratb@codingchick.net";
            CreatedBy = "Efrat Barak aka CodingChick";
            Version = "1.0.0.0";
        }

        public string Version { get; set; }

        public string EmailText { get; set; }
        public string CreatedBy { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string PrivcayPolicyLink { get; set; }
    }
}
