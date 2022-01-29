using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Lyric_Manager.Classes
{
    public static class SearchHandler
    {
        public static async Task<List<Song>> SearchMusic163(string SearchInput)
        {
            RestClient client = new RestClient("https://music.163.com/api/search/get");

            RestRequest request = new RestRequest();
            request.Method = Method.Post;
            request.AddQueryParameter("s", SearchInput);
            request.AddQueryParameter("type", "1");

            var response = await client.ExecuteAsync(request);

            try
            {
                Music163SearchJson Json = JsonConvert.DeserializeObject<Music163SearchJson>(response.Content);

                if (Json.result.songCount == 0)
                {
                    return null;
                }
                else
                {
                    //foreach(var song in Json.result.songs)
                    //{
                    //    song.album.picUrl = await GetMusic163ImageForResult(song.id.ToString());
                    //}

                    return Json.result.songs;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<string> DownloadFromMusic163(string id)
        {
            RestClient client = new RestClient("https://music.163.com/api/song/lyric");

            RestRequest request = new RestRequest();
            request.Method = Method.Get;
            request.AddQueryParameter("id", id);
            request.AddQueryParameter("lv", "-1");
            request.AddQueryParameter("kv", "-1");
            request.AddQueryParameter("tv", "-1");

            var response = await client.ExecuteAsync(request);

            try
            {
                Music163LyricJson Json = JsonConvert.DeserializeObject<Music163LyricJson>(response.Content);
                return Json.lrc.lyric;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static async Task SaveLyricsToFile(string Lyrics, string Filename, string DownloadPath)
        {

            StorageFolder Folder = await StorageFolder.GetFolderFromPathAsync(DownloadPath);
            StorageFile LyricFile = null;
            try
            {
                LyricFile = await Folder.CreateFileAsync($"{Filename}.lrc");
            }
            catch (Exception)
            {
                return;
            }

            await Windows.Storage.FileIO.WriteTextAsync(LyricFile, Lyrics);
        }
    }
}
