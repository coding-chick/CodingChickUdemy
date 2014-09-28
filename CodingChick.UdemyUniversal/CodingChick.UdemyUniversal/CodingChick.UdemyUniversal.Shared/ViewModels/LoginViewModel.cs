using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Input;
using Caliburn.Micro;
using CodingChick.UdemyUniversal.Core.Services;
using CodingChick.UdemyUniversal.CoreUI;

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
            _navigationService.NavigateToViewModel<CoursesViewModel>(_navigationService);

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
        private bool _signinButtonEnabled;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                CheckIfEnableSignIn();
            }
        }

        private void CheckIfEnableSignIn()
        {
            if (!string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Password))
            {
                SigninButtonEnabled = true;
            }
            else
            {
                SigninButtonEnabled = false;
            }
        }

        public bool SigninButtonEnabled
        {
            get { return _signinButtonEnabled; }
            set
            {
                _signinButtonEnabled = value;
                NotifyOfPropertyChange(() => SigninButtonEnabled);
            }
        }

        public async Task SignIn()
        {
            if (SigninButtonEnabled)
            {
                if (!string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Password))
                {
                    SigninButtonEnabled = false;
                    var loginSucceed = await _iOAuthService.GetUserToken(User, Password);

                    if (loginSucceed)
                        _navigationService.NavigateToViewModel<CoursesViewModel>(_navigationService);
                    else
                         await UiServices.ShowCustomMessage("Sorry, login was unsuccessful, please try again", "Login failed", "Ok", string.Empty,
                            new UICommand("Ok"), null);

                    SigninButtonEnabled = true;
                }
            }
        }

        public async void OkSignin(KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                if (SigninButtonEnabled)
                {
                   await SignIn();
                }
            }
        }
    }
}
