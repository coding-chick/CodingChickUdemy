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
        }

        void LecturePlayerView_Loaded(object sender, RoutedEventArgs e)
        {
            //This hack is because playlistplugin doesn't have a data context of its own and could not be bounded via xaml
            PlaylistPlugin plugin = (PlaylistPlugin) Player.Plugins.First();
            
            plugin.Playlist = ((LecturePlayerViewModel)this.DataContext).Parameter.LecturesPlaylist;
            plugin.CurrentPlaylistItem = ((LecturePlayerViewModel)this.DataContext).Parameter.CurrentPlaylistItem;
            plugin.CurrentPlaylistItemChanged += plugin_CurrentPlaylistItemChanged;

            ((IPlugin)plugin).Load();
        }

        void plugin_CurrentPlaylistItemChanged(object sender, EventArgs e)
        {
            ((LecturePlayerViewModel) this.DataContext).Parameter.CurrentPlaylistItem = ((PlaylistPlugin) sender).CurrentPlaylistItem;
          //
        }

        private void Player_OnMediaStarted(object sender, RoutedEventArgs e)
        {
            ((LecturePlayerViewModel)this.DataContext).PostStartedWatchingLecture();
        }

        private void Player_OnMediaEnded(object sender, MediaPlayerActionEventArgs e)
        {
            ((LecturePlayerViewModel)this.DataContext).PostFinishLecture();
        }
    }
}
