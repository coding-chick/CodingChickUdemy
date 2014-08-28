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
        private readonly IUdemyStorageManager _iUdemyStorageManager;
        private string _token;

        public OAuthService(IUdemyDataManager iUdemyDataManager, IUdemyStorageManager iUdemyStorageManager)
        {
            _iUdemyDataManager = iUdemyDataManager;
            _iUdemyStorageManager = iUdemyStorageManager;
        }

        public string Token
        {
            get
            {
                var token = _iUdemyStorageManager.GetValueFromSettings("token");
                //if (token != null)
                    return (string)token;
                //return string.Empty;
            }
            private set
            {
                _token = value;
                _iUdemyStorageManager.SaveValueToSettings("token", _token);

            }
        }

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