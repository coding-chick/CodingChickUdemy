using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodingChick.UdemyUniversal.Core.Base
{
    public class UdemyHttpEngine : IUdemyHttpEngine
    {
        private readonly IHttpClientAccessor _clientAccessor;

        public string ClientId { get { return "ad12eca9cbe17afac6259fe5d98471a6"; } }

        public string ClientSecret { get { return "a7c630646308824b2301fdb60ecfd8a0947e82d5"; } }

        public UdemyHttpEngine(IHttpClientAccessor clientAccessor)
        {
            _clientAccessor = clientAccessor;
        }

        public string BaseApiAddress
        {
            get { return "https://www.udemy.com/api-1.1/"; }
        }

        public async Task<HttpContent> GetAsyncWithToken(string method, List<KeyValuePair<string, string>> queryParams)
        {
            IDictionary<string, IEnumerable<string>> headers = new Dictionary<string, IEnumerable<string>>()
            {
                {"X-Udemy-Client-Id", new List<string>() {ClientId}},
                {"X-Udemy-Client-Secret", new List<string>() {ClientSecret}}
            };

            var finalAddress = HttpUtilityHelper.CreateFullAddess(BaseApiAddress, method, queryParams);
            var result = await _clientAccessor.GetAsync(finalAddress, headers);

            return result;
        }
    }
}
