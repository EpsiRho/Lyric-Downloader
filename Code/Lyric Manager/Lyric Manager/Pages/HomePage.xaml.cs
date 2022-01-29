using Lyric_Manager.Classes;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Lyric_Manager.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        // Page Vars
        ObservableCollection<object> SetupBreadcrumbs = new ObservableCollection<object>();
        ObservableCollection<object> SetupLibraries = new ObservableCollection<object>();
        ObservableCollection<object> ManualSearchResults = new ObservableCollection<object>();

        #region Page Loading
        public HomePage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await SettingsManager.GetSettings();
            if (SettingsManager.Settings == null)
            {
                SetupBreadcrumbs.Add(new Crumb("Welcome", null));
                SetupBackgroundGrid.Visibility = Visibility.Visible;
                SetupBorder.Visibility = Visibility.Visible;
            }

            if (SettingsManager.Libraries.Count > 0)
            {
                LibraryScanButton.IsEnabled = true;
            }
        }
        #endregion Page Loading

        #region OOB

        #region BreadCrumb Handling
        private void SetupBreadcrumBar_ItemClicked(BreadcrumbBar sender, BreadcrumbBarItemClickedEventArgs args)
        {
            SetupPivot.SelectedIndex = args.Index;
            try
            {
                while (true)
                {
                    SetupBreadcrumbs.RemoveAt(args.Index + 1);
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion BreadCrumb Handling

        #region Next Buttons
        private void StartSetupButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SetupBreadcrumbs.Add(new Crumb("Libraries", null));
            SetupPivot.SelectedIndex = 1;
        }
        private void LibrariesNextButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SetupBreadcrumbs.Add(new Crumb("Downloads", null));
            SetupPivot.SelectedIndex = 2;
        }
        private void DownloadsNextButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SetupBreadcrumbs.Add(new Crumb("Search", null));
            SetupPivot.SelectedIndex = 3;
        }
        private void SearchNextButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SetupBreadcrumbs.Add(new Crumb("Finished!", null));
            SetupPivot.SelectedIndex = 4;
        }

        #endregion Next Buttons

        #region Libraries
        private async void LibrariesAddButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FolderPicker();
            WinRT.Interop.InitializeWithWindow.Initialize(picker, ProcessHandler.hwnd);

            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add("*");

            StorageFolder SelectedFolder = await picker.PickSingleFolderAsync();
            if (SelectedFolder != null)
            {
                SetupLibraries.Add( SelectedFolder.Path);
                LibrariesNextButton.Content = "Next";
            }
        }
        private void LibrarySetupMenuFlyoutItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(e.OriginalSource != null)
            {
                SetupLibraries.Remove(((TextBlock)e.OriginalSource).DataContext);
            }

            if(SetupLibraries.Count == 0)
            {
                LibrariesNextButton.Content = "Skip";
            }
        }

        #endregion Libraries

        #region Downloads
        private async void DownloadsSetButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FolderPicker();
            WinRT.Interop.InitializeWithWindow.Initialize(picker, ProcessHandler.hwnd);

            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add("*");

            StorageFolder SelectedFolder = await picker.PickSingleFolderAsync();
            if (SelectedFolder != null)
            {
                DownloadDirectory.Text = SelectedFolder.Path;
                DownloadsNextButton.Visibility = Visibility.Visible;
            }
        }

        #endregion Downloads

        #region Search
        // Nothing here yet, since only Music163 is supported for now.
        #endregion Search

        #region SavingSetup
        private void CloseSetupButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CloseSetupButton.Content = new ProgressBar() { IsIndeterminate = true, Foreground = new SolidColorBrush(Color.FromArgb(255,255,255,255)), Width = 140};
            Thread t = new Thread(SaveSetupSettings);
            t.Start();
        }

        private void SaveSetupSettings()
        {
            string DownloadDirText = "";

            this.DispatcherQueue.TryEnqueue(() =>
            {
                DownloadDirText = DownloadDirectory.Text;
            });
            
            SettingsManager.AddSetting("DownloadFolder", DownloadDirText);
            SettingsManager.AddSetting("EnableMusic163", "True");

            if(SetupLibraries.Count > 0)
            {
                LibraryScanButton.IsEnabled = true;
            }

            foreach(string lib in SetupLibraries)
            {
                SettingsManager.AddLibrary(lib);
            }

            SettingsManager.SaveSettings();

            this.DispatcherQueue.TryEnqueue(() =>
            {
                SetupBorder.Visibility = Visibility.Collapsed;
                SetupBackgroundGrid.Visibility = Visibility.Collapsed;
            });
        }

        #endregion SavingSetup

        #endregion OOB

        #region Manual Search

        private void SearchBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                SearchBox.IsEnabled = false;
                ManualSearchResults.Clear();
                LyricPreviewBox.Text = "";
                NoResultsText.Visibility = Visibility.Collapsed;
                LyricPreviewBox.Visibility = Visibility.Collapsed;
                ManualSearchProgress.Visibility = Visibility.Visible;
                ManualSearchPanel.Visibility = Visibility.Visible;
                Thread t = new Thread(Search);
                t.Start(SearchBox.Text);
            }
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(SearchBox.Text == "")
            {
                ManualSearchResults.Clear();
                LyricPreviewBox.Text = "";
                ManualSearchPanel.Visibility = Visibility.Collapsed;
                LyricPreviewBox.Visibility = Visibility.Collapsed;
            }
        }

        private async void Search(object Input)
        {
            var Results = await SearchHandler.SearchMusic163((string)Input);

            if(Results.Count == 0)
            {
                this.DispatcherQueue.TryEnqueue(() =>
                {
                    NoResultsText.Visibility = Visibility.Visible;
                });
            }

            foreach (var res in Results)
            {
                this.DispatcherQueue.TryEnqueue(() =>
                {
                    ManualSearchResults.Add(res);
                });
            }

            this.DispatcherQueue.TryEnqueue(() =>
            {
                SearchBox.IsEnabled = true;
                ManualSearchProgress.Visibility = Visibility.Collapsed;
            });
        }
        private void SearchListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Song s = e.ClickedItem as Song;

            if(SearchListView.SelectedItem == s)
            {
                LyricPreviewBox.Visibility = Visibility.Collapsed;
                LyricsPreviewProgress.Visibility = Visibility.Collapsed;
                SearchListView.SelectionMode = ListViewSelectionMode.None;
            }
            else
            {
                LyricPreviewBox.Text = "";
                LyricPreviewBox.Visibility = Visibility.Visible;
                LyricsPreviewProgress.Visibility = Visibility.Visible;
                SearchListView.SelectionMode = ListViewSelectionMode.Single;
                SearchListView.SelectedItem = s;
                Thread t = new Thread(GetLyricsThreaded);
                t.Start(s.id.ToString());
            }
        }

        private async void GetLyricsThreaded(object SongId)
        {
            string lyrics = await SearchHandler.DownloadFromMusic163((string)SongId);

            this.DispatcherQueue.TryEnqueue(() =>
            {
                LyricPreviewBox.Text = lyrics;
                LyricsPreviewProgress.Visibility = Visibility.Collapsed;
            });
        }


        #endregion Manual Search

    }
}
