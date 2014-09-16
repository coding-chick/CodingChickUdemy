using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using CodingChick.UdemyUniversal.ViewModels;
using Microsoft.PlayerFramework;

namespace CodingChick.UdemyUniversal.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LecturePlayerView : Page
    {
        public LecturePlayerView()
        {
            this.InitializeComponent();
            this.Loaded += LecturePlayerView_Loaded;
            //BindingOperations.SetBinding(PlaylistPlugin, PlaylistPlugin.PlaylistProperty, new Binding() { Path = new PropertyPath("LecturesPlaylist"), Source = this.DataContext });
        }

        void LecturePlayerView_Loaded(object sender, RoutedEventArgs e)
        {
            PlaylistPlugin plugin = (PlaylistPlugin) Player.Plugins.First();
            
            plugin.Playlist = ((LecturePlayerViewModel)this.DataContext).LecturesPlaylist;
            plugin.CurrentPlaylistItem = ((LecturePlayerViewModel)this.DataContext).CurrentLecture;
            plugin.CurrentPlaylistItemChanged += plugin_CurrentPlaylistItemChanged;

            ((IPlugin)plugin).Load();
        }

        void plugin_CurrentPlaylistItemChanged(object sender, EventArgs e)
        {
            //PlaylistPlugin.PreviousPlaylistItem
            ((LecturePlayerViewModel)this.DataContext).PostLecturePosition(sender, e);
        }

    }
}
