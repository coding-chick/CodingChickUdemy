using System;
using System.Text;
using System.Threading.Tasks;

namespace CodingChick.UdemyUniversal.Core.Services
{
    public interface IOAuthService
    {
        Task<bool> GetUserToken(string userEmail, string password);
    }
}
