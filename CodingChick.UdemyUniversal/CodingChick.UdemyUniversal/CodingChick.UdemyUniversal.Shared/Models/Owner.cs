using Caliburn.Micro;
using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class User : PropertyChangedBase
    {
        private string _description;
        //[JsonProperty("img_480x270")]
        //public string __class { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string JobTitle { get; set; }

        public string Description   
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }

        public string Url { get; set; }
        public SimpleResolutionImages Images { get; set; }
    }
}