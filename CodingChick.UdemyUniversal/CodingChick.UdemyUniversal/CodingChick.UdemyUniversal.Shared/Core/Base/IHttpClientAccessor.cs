using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CodingChick.UdemyUniversal.Core.Base
{
    public interface IHttpClientAccessor
    {
        Task<HttpContent> GetAsync(string address);
        Task<HttpContent> GetAsync(string address, IDictionary<string, IEnumerable<string>> headers);
        Task<HttpContent> PutAsync(string address, HttpContent content, string charSet = "",
            string mediaType = "");
        Task<HttpContent> DeleteAsync(string address);
        Task<HttpContent> DeleteAsync(string address, IDictionary<string, IEnumerable<string>> headers);
        Task<HttpResponseHeaders> GetHeaderAsync(string finalAddress);

        Task<HttpContent> PostAsync(string address, HttpContent content,
            IDictionary<string, IEnumerable<string>> headers,
            string charSet = "",
            string mediaType = "");
    }
}