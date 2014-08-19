using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodingChick.UdemyUniversal.Core.Base
{
    public interface IUdemyHttpEngine
    {
        Task<HttpContent> GetAsyncWithToken(string method, List<KeyValuePair<string, string>> queryParams);
    }
}