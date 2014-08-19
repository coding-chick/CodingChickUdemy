using System.Collections.Generic;
using System.Threading.Tasks;
using CodingChick.UdemyUniversal.Core.Base;
using CodingChick.UdemyUniversal.Models;
using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Core.Services
{
    public class OAuthService : IOAuthService
    {
        private readonly IUdemyHttpEngine _iUdemyHttpEngine;

        public OAuthService(IUdemyHttpEngine iUdemyHttpEngine)
        {
            _iUdemyHttpEngine = iUdemyHttpEngine;
        }

        public string Token { get; private set; }

        public async Task<bool> GetUserToken(string userEmail, string password)
        {
            List<KeyValuePair<string, string>> methodParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("email", userEmail),
                new KeyValuePair<string, string>("password", password)
            };

            var httpResult = await _iUdemyHttpEngine.GetAsyncWithToken("oauth", methodParams);
            var httpString = await httpResult.ReadAsStringAsync();
            var tokenResult = JsonConvert.DeserializeObject<TokenData>(httpString);
            if (tokenResult.Error == null && !string.IsNullOrEmpty(tokenResult.Token))
            {
                Token = tokenResult.Token;
                return false;
            }
            return true;
        }
    }
}