using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lyric_Manager.Classes
{
    public class Artist
    {
        public int id { get; set; }
        public string name { get; set; }
        public object picUrl { get; set; }
        public List<object> alias { get; set; }
        public int albumSize { get; set; }
        public int picId { get; set; }
        public string img1v1Url { get; set; }
        public int img1v1 { get; set; }
        public object trans { get; set; }
        public int img1v1Id { get; set; }
        public string briefDesc { get; set; }
        public int musicSize { get; set; }
        public int topicPerson { get; set; }
    }

    public class Artist2
    {
        public int id { get; set; }
        public string name { get; set; }
        public object picUrl { get; set; }
        public List<object> alias { get; set; }
        public int albumSize { get; set; }
        public int picId { get; set; }
        public string img1v1Url { get; set; }
        public int img1v1 { get; set; }
        public object trans { get; set; }
        public int img1v1Id { get; set; }
        public string briefDesc { get; set; }
        public int musicSize { get; set; }
        public int topicPerson { get; set; }
    }

    public class Album : INotifyPropertyChanged
    {
        // UI notification functions (Ping the UI to change when some properties change)
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public int id { get; set; }
        public string name { get; set; }
        public Artist artist { get; set; }
        public object publishTime { get; set; }
        public int size { get; set; }
        public int copyrightId { get; set; }
        public int status { get; set; }
        public object picId { get; set; }
        public int mark { get; set; }
        public string blurPicUrl { get; set; }
        public int companyId { get; set; }
        public long pic { get; set; }
        private string PicUrl { get; set; }
        public string picUrl
        {
            get
            {
                return PicUrl;
            }
            set
            {
                if (value != "Microsoft.UI.Xaml.Media.Imaging.BitmapImage")
                {
                    PicUrl = value;
                    OnPropertyChanged("picUrl");
                }
            }
        }

        public string description { get; set; }
        public string tags { get; set; }
        public string company { get; set; }
        public string briefDesc { get; set; }
        public List<object> songs { get; set; }
        public List<object> alias { get; set; }
        public string commentThreadId { get; set; }
        public List<Artist> artists { get; set; }
        public string subType { get; set; }
        public object transName { get; set; }
        public bool onSale { get; set; }
        public int gapless { get; set; }
        public string picId_str { get; set; }
    }

    public class Song
    {
        public string name { get; set; }
        public int id { get; set; }
        public int position { get; set; }
        public List<object> alias { get; set; }
        public int status { get; set; }
        public int fee { get; set; }
        public int copyrightId { get; set; }
        public string disc { get; set; }
        public int no { get; set; }
        public List<Artist> artists { get; set; }
        public Album album { get; set; }
        public bool starred { get; set; }
        public double popularity { get; set; }
        public int score { get; set; }
        public int starredNum { get; set; }
        public int duration { get; set; }
        public int playedNum { get; set; }
        public int dayPlays { get; set; }
        public int hearTime { get; set; }
        public string ringtone { get; set; }
        public object crbt { get; set; }
        public object audition { get; set; }
        public string copyFrom { get; set; }
        public string commentThreadId { get; set; }
        public object rtUrl { get; set; }
        public int ftype { get; set; }
        public List<object> rtUrls { get; set; }
        public int copyright { get; set; }
        public object transName { get; set; }
        public object sign { get; set; }
        public int mark { get; set; }
        public int originCoverType { get; set; }
        public object originSongSimpleData { get; set; }
        public int single { get; set; }
        public object noCopyrightRcmd { get; set; }
        public HMusic hMusic { get; set; }
        public int rtype { get; set; }
        public object rurl { get; set; }
        public MMusic mMusic { get; set; }
        public LMusic lMusic { get; set; }
        public BMusic bMusic { get; set; }
        public int mvid { get; set; }
        public object mp3Url { get; set; }
        public object rUrl { get; set; }
    }

    public class Result
    {
        public List<Song> songs { get; set; }
        public bool hasMore { get; set; }
        public int songCount { get; set; }
    }

    public class Music163SearchJson
    {
        public Result result { get; set; }
        public int code { get; set; }
    }

}
