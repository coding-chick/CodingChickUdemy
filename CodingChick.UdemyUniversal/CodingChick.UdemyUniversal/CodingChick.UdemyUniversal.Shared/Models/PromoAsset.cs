﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class PromoAsset
    {
        // public string __class { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string ContextInfo { get; set; }
        public string StreamUrl { get; set; }
        public DownloadUrl DownloadUrl { get; set; }
        public Data Data { get; set; }
        public List<object> Captions { get; set; }
    }
}