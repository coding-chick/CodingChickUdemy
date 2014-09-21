using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CodingChick.UdemyUniversal.CoreUI
{
    public class WebBrowserExtentions
    {
        public static string GetHTML(DependencyObject obj)
        {
            return (string)obj.GetValue(HTMLProperty);
        }

        public static void SetHTML(DependencyObject obj, string value)
        {
            obj.SetValue(HTMLProperty, value);
        }

        // Using a DependencyProperty as the backing store for HTML.  This enables animation, styling, binding, etc... 
        public static readonly DependencyProperty HTMLProperty =
            DependencyProperty.RegisterAttached("HTML", typeof(string), typeof(WebBrowserExtentions), new PropertyMetadata(0, new PropertyChangedCallback(OnHTMLChanged)));

        private static void OnHTMLChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var htmlString = (string) e.NewValue;
            htmlString = "<Html><Body>" + htmlString + "</Body></Html>";
            WebView wv = d as WebView;
            if (wv != null)
            {
                //var dataTransferPackage = new DataTransferManager();
                //dataTransferPackage.
                //wv.DataTransferPackage.SetHtmlFormat(htmlString);
                wv.NavigateToString(htmlString);
            }
        }

        
    }
}
