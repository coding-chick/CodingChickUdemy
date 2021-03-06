﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CodingChick.UdemyUniversal.Core.Base.Helpers;

namespace CodingChick.UdemyUniversal.Core.Base
{
    public class UdemyHttpEngine : IUdemyHttpEngine
    {
        private readonly IHttpClientAccessor _clientAccessor;

        public string ClientId { get { return ""; } }

        public string ClientSecret { get { return ""; } }

        public UdemyHttpEngine(IHttpClientAccessor clientAccessor)
        {
            _clientAccessor = clientAccessor;
        }

        public string BaseApiAddress
        {
            get { return "https://www.udemy.com/api-1.1/"; }
        }

        public async Task<HttpContent> GetAsyncWithIdSecret(string method, List<KeyValuePair<string, string>> queryParams)
        {
            IDictionary<string, IEnumerable<string>> headers = new Dictionary<string, IEnumerable<string>>()
            {
                {"X-Udemy-Client-Id", new List<string>() {ClientId}},
                {"X-Udemy-Client-Secret", new List<string>() {ClientSecret}}
            };

            var result = await GetHttpContent(method, queryParams, headers);
            return result;
        }

        private async Task<HttpContent> GetHttpContent(string method, List<KeyValuePair<string, string>> queryParams, IDictionary<string, IEnumerable<string>> headers)
        {
            var finalAddress = HttpUtilityHelper.CreateFullAddess(BaseApiAddress, method, queryParams);
            var result = await _clientAccessor.GetAsync(finalAddress, headers);
            return result;
        }

        public async Task<HttpContent> GetAsyncWithIdToken(string method, List<KeyValuePair<string, string>> queryParams, string token)
        {
            var headers = CreateClientIdTokenHeaders(token);

            var result = await GetHttpContent(method, queryParams, headers);
            return result;
        }

        private IDictionary<string, IEnumerable<string>> CreateClientIdTokenHeaders(string token)
        {
            IDictionary<string, IEnumerable<string>> headers = new Dictionary<string, IEnumerable<string>>()
            {
                {"X-Udemy-Client-Id", new List<string>() {ClientId}},
                {"X-Udemy-Bearer-Token", new List<string>() {token}}
            };
            return headers;
        }

        public async Task<HttpContent> PostAsync(string method, string token)
        {
            var headers = CreateClientIdTokenHeaders(token);
            var finalAddress = HttpUtilityHelper.CreateFullAddess(BaseApiAddress, method, new List<KeyValuePair<string, string>>());
            var httpContent = new StringContent(string.Empty);
            return await _clientAccessor.PostAsync(finalAddress, httpContent, headers);
        }
    }
}
