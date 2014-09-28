using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227
using Caliburn.Micro;
using Caliburn.Micro.Logging;
using CodingChick.UdemyUniversal.Core.Base;
using CodingChick.UdemyUniversal.Core.Services;
using CodingChick.UdemyUniversal.Views;
using CodingChick.UdemyUniversal.ViewModels;

namespace CodingChick.UdemyUniversal
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App
    {
        private WinRTContainer container;

        public App()
        {
            InitializeComponent();
            LogManager.GetLog = type => new DebugLogger(type);
        }

        protected override void Configure()
        {
            container = new WinRTContainer();

            container.RegisterWinRTServices();

            container.PerRequest<LoginViewModel>();
            container.PerRequest<CoursesViewModel>();
            container.PerRequest<CourseDetailsViewModel>();
            container.PerRequest<LecturePlayerViewModel>();
            container.PerRequest<CoursesListViewModel>();
            container.PerRequest<AboutViewModel>();

            container.Singleton<IOAuthService, OAuthService>();
            container.PerRequest<IUdemyDataManager, UdemyDataManager>();
            container.PerRequest<IDataService, DataService>();
            container.PerRequest<IHttpClientAccessor, HttpClientAccessor>();
            container.PerRequest<IUdemyHttpEngine, UdemyHttpEngine>();
            container.PerRequest<IUdemyStorageManager, UdemyStorageManager>();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            container.RegisterNavigationService(rootFrame);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
#if WINDOWS_PHONE_APP
            // makes awesome SystemTray merge with the content
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
#endif
            //yes we check directly to the API, life's hard without container being up yet so no IoC
            var token = ApplicationData.Current.LocalSettings.Values["token"] as string;
            if (!string.IsNullOrEmpty(token))
                DisplayRootView<CoursesView>();
            else
                DisplayRootView<LoginView>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);
        }
    }
}