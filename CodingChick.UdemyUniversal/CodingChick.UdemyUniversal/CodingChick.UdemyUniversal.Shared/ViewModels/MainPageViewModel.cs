using Caliburn.Micro;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class MainPageViewModel : Screen
    {
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        private string _test;
        public string Test
        {
            get { return _test; }
            set
            {
                _test = value;
                NotifyOfPropertyChange(() => Test);
            }
        }

        public void EmailSignIn()
        {
            _navigationService.NavigateToViewModel<LoginViewModel>(_navigationService);
        }
    }
}
