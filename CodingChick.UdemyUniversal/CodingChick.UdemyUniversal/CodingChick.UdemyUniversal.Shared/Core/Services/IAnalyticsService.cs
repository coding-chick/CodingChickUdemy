using System;

namespace CodingChick.UdemyUniversal.Core.Services
{
    public interface IAnalyticsService
    {
        void SendView(string screenUri);
        void SendException(Exception e);
    }
}