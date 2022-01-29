using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Lyric_Manager.Classes
{
    public static class SettingsManager
    {
        public static bool IsSettingsLoaded { get; set; }
        public static Dictionary<string, string> Settings { get; set; }
        public static List<string> Libraries { get; set; }

        public static void AddSetting(string Tag, string Value)
        {
            if (Settings == null)
            {
                Settings = new Dictionary<string, string>();
            }

            Settings.Add(Tag, Value);
        }

        public static void AddLibrary(string Lib)
        {
            if(Libraries == null)
            {
                Libraries = new List<string>();
            }

            Libraries.Add(Lib);
        }
        public static async Task SaveSettings()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string[] dirs = Directory.GetDirectories(path);
            if (!dirs.Contains("LyricManager"))
            {
                System.IO.Directory.CreateDirectory($"{path}\\LyricManager");
            }

            StorageFolder Folder = await StorageFolder.GetFolderFromPathAsync($"{path}\\LyricManager");
            StorageFile SettingsFile = null;
            try
            {
                SettingsFile = await Folder.GetFileAsync($"Settings");
            }
            catch (Exception)
            {
                return;
            }

            string serial = JsonConvert.SerializeObject(Settings);
            await Windows.Storage.FileIO.WriteTextAsync(SettingsFile, serial);

            StorageFile LibrariesFile = null;
            try
            {
                LibrariesFile = await Folder.GetFileAsync($"Libraries");
            }
            catch (Exception)
            {
                return;
            }

            serial = JsonConvert.SerializeObject(Libraries);
            await Windows.Storage.FileIO.WriteTextAsync(LibrariesFile, serial);
        }
        public static async Task GetSettings()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string[] dirs = Directory.GetDirectories(path);
            if (!dirs.Contains("LyricManager"))
            {
                System.IO.Directory.CreateDirectory($"{path}\\LyricManager");
            }

            StorageFolder Folder = await StorageFolder.GetFolderFromPathAsync($"{path}\\LyricManager");
            StorageFile SettingsFile = null;
            try
            {
                SettingsFile = await Folder.GetFileAsync($"Settings");
            }
            catch (Exception)
            {
                SettingsFile = await Folder.CreateFileAsync($"Settings");
                return;
            }

            Settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(await Windows.Storage.FileIO.ReadTextAsync(SettingsFile, Windows.Storage.Streams.UnicodeEncoding.Utf8));

            StorageFile LibrariesFile = null;
            try
            {
                LibrariesFile = await Folder.GetFileAsync($"Libraries");
            }
            catch (Exception)
            {
                LibrariesFile = await Folder.CreateFileAsync($"Libraries");
                return;
            }

            Libraries = JsonConvert.DeserializeObject<List<string>>(await Windows.Storage.FileIO.ReadTextAsync(LibrariesFile, Windows.Storage.Streams.UnicodeEncoding.Utf8));
        }
    }
}
