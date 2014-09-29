using System.Collections.Generic;
using Caliburn.Micro;
using Newtonsoft.Json;
using Telerik.Universal.Core;

namespace CodingChick.UdemyUniversal.Models
{
    public class Course :PropertyChangedBase
    {
        private string _description;
        //public string __class { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Headline { get; set; }
        public int NumLectures { get; set; }
        public int NumSubscribers { get; set; }
        public string ContentInfo { get; set; }
        public string OriginalPriceText { get; set; }
        public int NumOfReviews { get; set; }
        public int NumOfTextReviews { get; set; }
        public double AvgRating { get; set; }
        public double AvgRatingRounded { get; set; }
        public string Price { get; set; }
        public User User { get; set; }
        public AdvancedResolutionImages Images { get; set; }
        public bool IsPaid { get; set; }
        public List<User> VisibleInstructors { get; set; }
        public string InstructionalLevel { get; set; }
        public PromoAsset PromoAsset { get; set; }
        public string Url { get; set; }
        public object NumInvitationRequests { get; set; }
        public string IsPublished { get; set; }
        public bool IsInAppPurchaseEnabled { get; set; }
        public bool IsGoogleInAppPurchaseEnabled { get; set; }
        public bool FavoriteTime { get; set; }
        public bool ArchiveTime { get; set; }
        public string GoogleInAppPurchasePriceText { get; set; }
        public string InAppPurchasePriceText { get; set; }
        public bool IsAvaliableOnIos { get; set; }
        public bool HasCertificate { get; set; }
        public string Locale { get; set; }
        public string Status { get; set; }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }       
    }
}