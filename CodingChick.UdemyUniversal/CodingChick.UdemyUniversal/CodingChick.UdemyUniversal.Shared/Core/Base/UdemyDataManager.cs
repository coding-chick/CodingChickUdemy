using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodingChick.UdemyUniversal.Core.Base.Helpers;
using CodingChick.UdemyUniversal.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CodingChick.UdemyUniversal.Core.Base
{
    public class UdemyDataManager : IUdemyDataManager
    {
        private readonly IUdemyHttpEngine _udemyHttpEngine;

        public UdemyDataManager(IUdemyHttpEngine udemyHttpEngine)
        {
            _udemyHttpEngine = udemyHttpEngine;
        }

        public async Task<T> GetDataAsync<T>(string method, List<KeyValuePair<string, string>> queryParams)
        {
            var httpResult = await _udemyHttpEngine.GetAsyncWithIdSecret(method, queryParams);
            var httpString = await httpResult.ReadAsStringAsync();
            var dataResult = JsonConvert.DeserializeObject<T>(httpString);
            return dataResult;
        }

        public async Task<T> GetDataAsync<T>(string method, List<KeyValuePair<string, string>> queryParams, string token)
        {
            var httpString = await GetContentStringFromHttp<T>(method, queryParams, token);
            var dataResult = JsonConvert.DeserializeObject<T>(httpString);
            return dataResult;
        }

        private async Task<string> GetContentStringFromHttp<T>(string method, List<KeyValuePair<string, string>> queryParams, string token)
        {
            var httpResult = await _udemyHttpEngine.GetAsyncWithIdToken(method, queryParams, token);
            var httpString = await httpResult.ReadAsStringAsync();
            return httpString;
        }

        public async Task<T> GetDataAsyncWithConverter<T>(string method, List<KeyValuePair<string, string>> queryParams,
            string token)
        {
            var httpString = await GetContentStringFromHttp<T>(method, queryParams, token);
            var dataResult = JsonConvert.DeserializeObject<T>(httpString, new BaseDataConverter());
            return dataResult;
        }

        public async Task<T> PostDataAsync<T>(string method, string token)
        {
            var httpResult = await _udemyHttpEngine.PostAsync(method, token);
            var httpString = await httpResult.ReadAsStringAsync();            
            var dataResult = JsonConvert.DeserializeObject<T>(httpString);
            return dataResult;
        }
    }
}
