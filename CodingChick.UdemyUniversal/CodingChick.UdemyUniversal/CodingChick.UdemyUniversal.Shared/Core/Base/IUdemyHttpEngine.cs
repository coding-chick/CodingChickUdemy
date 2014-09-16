using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodingChick.UdemyUniversal.Core.Base
{
    public interface IUdemyHttpEngine
    {
        Task<HttpContent> GetAsyncWithIdSecret(string method, List<KeyValuePair<string, string>> queryParams);
        Task<HttpContent> GetAsyncWithIdToken(string method, List<KeyValuePair<string, string>> queryParams, string token);
        Task<HttpContent> PostAsync(string method, string token);
    }
}