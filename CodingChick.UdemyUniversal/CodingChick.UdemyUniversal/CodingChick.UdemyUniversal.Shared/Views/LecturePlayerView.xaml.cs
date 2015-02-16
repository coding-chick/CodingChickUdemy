using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.System.Display;
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
            this.Unloaded +=LecturePlayerView_Unloaded;
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
        }

        void LecturePlayerView_Unloaded(object sender, RoutedEventArgs e)
        {
            _keepScreenOnRequest.RequestRelease();
        }

        private Windows.System.Display.DisplayRequest _keepScreenOnRequest;
        void LecturePlayerView_Loaded(object sender, RoutedEventArgs e)
        {
            _keepScreenOnRequest = new Windows.System.Display.DisplayRequest();
            _keepScreenOnRequest.RequestActive();

            //This hack is because playlistplugin doesn't have a data context of its own and could not be bounded via xaml
            PlaylistPlugin plugin = (PlaylistPlugin) Player.Plugins.First();
            
            plugin.Playlist = ((LecturePlayerViewModel)this.DataContext).Parameter.LecturesPlaylist;
            plugin.CurrentPlaylistItem = ((LecturePlayerViewModel)this.DataContext).Parameter.CurrentPlaylistItem;
            plugin.CurrentPlaylistItemChanged += plugin_CurrentPlaylistItemChanged;
            
            ((LecturePlayerViewModel)this.DataContext).Parameter.OnCurrentPlaylistItemChanged += Parameter_OnCurrentPlaylistItemChanged;
            
            ((IPlugin)plugin).Load();
        }

        void Parameter_OnCurrentPlaylistItemChanged(object sender, EventArgs e)
        {
            ((LecturePlayerViewModel) this.DataContext).Parameter.ChangedPlaylistItemFromCode = false;

            PlaylistPlugin plugin = (PlaylistPlugin)Player.Plugins.First();
            plugin.CurrentPlaylistItem = ((LecturePlayerViewModel)this.DataContext).Parameter.CurrentPlaylistItem;
        }

        void plugin_CurrentPlaylistItemChanged(object sender, EventArgs e)
        {
            ((LecturePlayerViewModel)this.DataContext).Parameter.NewPlaylistItem = ((PlaylistPlugin)sender).CurrentPlaylistItem;
            if (((PlaylistPlugin) sender).NextPlaylistItem == null)
            {
                ((LecturePlayerViewModel) this.DataContext).ReachedEndNavigateBack();
            }
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
