using Caliburn.Micro;
using CodingChick.UdemyUniversal.Core.Services;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class MainPageViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IOAuthService _iOAuthService;

        public MainPageViewModel(INavigationService navigationService, IOAuthService iOAuthService)
        {
            _navigationService = navigationService;
            _iOAuthService = iOAuthService;          
        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            if (!string.IsNullOrEmpty(_iOAuthService.Token))
            {
                _navigationService.NavigateToViewModel<CoursesViewModel>(_navigationService);
            }
        }

        public void EmailSignIn()
        {
            _navigationService.NavigateToViewModel<LoginViewModel>(_navigationService);
        }
    }
}
