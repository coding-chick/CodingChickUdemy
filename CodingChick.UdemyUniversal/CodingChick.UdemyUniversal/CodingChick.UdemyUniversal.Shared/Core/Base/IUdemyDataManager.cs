using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingChick.UdemyUniversal.Core.Base
{
    public interface IUdemyDataManager
    {
        Task<T> GetDataAsync<T>(string method, List<KeyValuePair<string, string>> queryParams, string token);
        Task<T> GetDataAsync<T>(string method, List<KeyValuePair<string, string>> queryParams);

        Task<T> GetDataAsyncWithConverter<T>(string method, List<KeyValuePair<string, string>> queryParams,
            string token);

        Task<T> PostDataAsync<T>(string method, string token);
    }
}