using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Models
{
    public class TokenRoot
    {
        public string Token { get; set; }

        public Error Error { get; set; }
    }
}