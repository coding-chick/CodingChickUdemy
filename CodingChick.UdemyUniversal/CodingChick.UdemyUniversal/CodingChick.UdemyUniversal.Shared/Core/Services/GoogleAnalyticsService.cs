using System;

namespace CodingChick.UdemyUniversal.Core.Services
{
    class GoogleAnalyticsService : IAnalyticsService
    {
        public void SendView(string screenUri)
        {
            try
            {
                GoogleAnalytics.EasyTracker.GetTracker().SendView(screenUri);
            }
            catch (Exception)
            {                
            }
        }

        public void SendException(Exception e)
        {
            try
            {
                if (e != null)
                {
                    string exceptionString =
                        string.Format("Exception of type {0} With the message {1} And the stack trace was {2}",
                            e.GetType(), e.Message, e.StackTrace);
                    GoogleAnalytics.EasyTracker.GetTracker().SendException(exceptionString, true);

                    var innerException = e.InnerException;
                    while (innerException != null)
                    {
                        exceptionString =
                            string.Format(
                                "Inner exception was:Exception of type {0} With the message {1} And the stack trace was {2}",
                                innerException.GetType(), innerException.Message, innerException.StackTrace);
                        GoogleAnalytics.EasyTracker.GetTracker().SendException(exceptionString, true);
                        innerException = innerException.InnerException;
                    }
                }
                else
                {
                    GoogleAnalytics.EasyTracker.GetTracker()
                        .SendException("Something unexpected happened and we have no idea what", true);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}