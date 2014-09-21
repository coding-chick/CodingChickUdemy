using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.ViewManagement;

namespace CodingChick.UdemyUniversal.CoreUI
{
    public class UiServicesss
    {
        public static void ShowCustomMessage(string messageContent, string messageTitle, string confirmButtonText,
            string cancelButtonText, UICommand action, UICommand actionCancel)
        {
            CoreDispatcher dispatcher = Window.Current == null ? null : Window.Current.Dispatcher;

            MessageDialog messageDialog = new MessageDialog(messageContent);
            messageDialog.Commands.Add(new UICommand(confirmButtonText, action.Invoked));
            messageDialog.Commands.Add(new UICommand(cancelButtonText, actionCancel.Invoked));
            messageDialog.Title = messageTitle;

            if (dispatcher != null)
                dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => messageDialog.ShowAsync());
            else
                messageDialog.ShowAsync();
        }


        public static async Task ShowProgressIndicatorForAction(Action action)
        {
            CoreDispatcher dispatcher = Window.Current == null ? null : Window.Current.Dispatcher;

            if (dispatcher != null)
                dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => await RunStatusBarAndAction(action));
            else
                await RunStatusBarAndAction(action);
        }

        private static async Task RunStatusBarAndAction(Action action)
        {
#if WINDOWS_PHONE_APP
            StatusBar.GetForCurrentView().BackgroundOpacity = 0;
            StatusBar.GetForCurrentView().ProgressIndicator.Text = "Loading...";
            await StatusBar.GetForCurrentView().ProgressIndicator.ShowAsync();
#endif
            action();
#if WINDOWS_PHONE_APP
            await StatusBar.GetForCurrentView().ProgressIndicator.HideAsync();
#endif
        }
    }
}
