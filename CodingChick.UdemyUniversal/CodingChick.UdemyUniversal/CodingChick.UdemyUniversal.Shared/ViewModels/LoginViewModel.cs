using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using CodingChick.UdemyUniversal.Core.Services;

namespace CodingChick.UdemyUniversal.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly IOAuthService _iOAuthService;
        private readonly INavigationService _navigationService;

        public LoginViewModel(INavigationService navigationService, IOAuthService iOAuthService)
        {
            _navigationService = navigationService;
            _iOAuthService = iOAuthService;
        }

        private string _user;
        public string User
        {
            get { return _user; }
            set
            {
                _user = value;
                NotifyOfPropertyChange(() => User);
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        public async void SignIn()
        {
            var loginSucceed = await _iOAuthService.GetUserToken(User, Password);

            if (loginSucceed)
                _navigationService.NavigateToViewModel<CoursesViewModel>(_navigationService);
        }
    }
}
