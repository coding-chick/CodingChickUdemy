using System.Collections.Generic;
using System.Threading.Tasks;
using CodingChick.UdemyUniversal.Core.Base;
using CodingChick.UdemyUniversal.Models;
using Newtonsoft.Json;

namespace CodingChick.UdemyUniversal.Core.Services
{
    public class OAuthService : IOAuthService
    {
        private readonly IUdemyDataManager _iUdemyDataManager;

        public OAuthService(IUdemyDataManager iUdemyDataManager)
        {
            _iUdemyDataManager = iUdemyDataManager;
        }

        public string Token { get; private set; }

        public async Task<bool> GetUserToken(string userEmail, string password)
        {
            List<KeyValuePair<string, string>> methodParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("email", userEmail),
                new KeyValuePair<string, string>("password", password)
            };

            var tokenResult = await _iUdemyDataManager.GetDataAsync<TokenRoot>("oauth/token", methodParams);
            if (tokenResult.Error == null && !string.IsNullOrEmpty(tokenResult.Token))
            {
                Token = tokenResult.Token;
                return true;
            }
            return false;
        }
    }
}