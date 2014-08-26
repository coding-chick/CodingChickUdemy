using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class Course
    {
        //public string __class { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("headline")]
        public string Headline { get; set; }
        [JsonProperty("numLectures")]
        public int NumLectures { get; set; }
        [JsonProperty("numSubscribers")]
        public int NumSubscribers { get; set; }
        [JsonProperty("contentInfo")]
        public string ContentInfo { get; set; }
        [JsonProperty("originalPriceText")]
        public string OriginalPriceText { get; set; }
        [JsonProperty("numOfReviews")]
        public int NumOfReviews { get; set; }
        [JsonProperty("numOfTextReviews")]
        public int NumOfTextReviews { get; set; }
        [JsonProperty("avgRating")]
        public double AvgRating { get; set; }
        [JsonProperty("avgRatingRounded")]
        public double AvgRatingRounded { get; set; }
        [JsonProperty("price")]
        public string Price { get; set; }
        public User User { get; set; }
        [JsonProperty("images")]
        public AdvancedResolutionImages Images { get; set; }
        [JsonProperty("isPaid")]
        public bool IsPaid { get; set; }
        [JsonProperty("visibleInstructors")]
        public List<User> VisibleInstructors { get; set; }
        [JsonProperty("instructionalLevel")]
        public string InstructionalLevel { get; set; }
        [JsonProperty("promoAsset")]
        public PromoAsset PromoAsset { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("numInvitationRequests")]
        public object NumInvitationRequests { get; set; }
        [JsonProperty("isPublished")]
        public string IsPublished { get; set; }
        [JsonProperty("isInAppPurchaseEnabled")]
        public bool IsInAppPurchaseEnabled { get; set; }
        [JsonProperty("isGoogleInAppPurchaseEnabled")]
        public bool IsGoogleInAppPurchaseEnabled { get; set; }
        [JsonProperty("favoriteTime")]
        public bool FavoriteTime { get; set; }
        [JsonProperty("archiveTime")]
        public bool ArchiveTime { get; set; }
        [JsonProperty("googleInAppPurchasePriceText")]
        public string GoogleInAppPurchasePriceText { get; set; }
        [JsonProperty("inAppPurchasePriceText")]
        public string InAppPurchasePriceText { get; set; }
        [JsonProperty("isAvaliableOnIos")]
        public bool IsAvaliableOnIos { get; set; }
        [JsonProperty("hasCertificate")]
        public bool HasCertificate { get; set; }
        [JsonProperty("locale")]
        public string Locale { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}