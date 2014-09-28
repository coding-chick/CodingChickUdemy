using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using CodingChick.UdemyUniversal.Core.Services;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class ScreenWithAnalytics : Screen
    {
        public ScreenWithAnalytics()
        {
        }

        private readonly IAnalyticsService _analyticsService;

        public ScreenWithAnalytics(INavigationService navigation, IAnalyticsService analyticsService)
        {
            navigation.Navigated += _navigation_Navigated;
            _analyticsService = analyticsService;
        }

        void _navigation_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            _analyticsService.SendView(e.SourcePageType.ToString());
        }
    }
}
