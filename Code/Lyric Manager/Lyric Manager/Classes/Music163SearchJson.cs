using System;
using System.Collections.Generic;
using System.Linq;
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
    }

    public class Album
    {
        public int id { get; set; }
        public string name { get; set; }
        public Artist artist { get; set; }
        public object publishTime { get; set; }
        public int size { get; set; }
        public int copyrightId { get; set; }
        public int status { get; set; }
        public object picId { get; set; }
        public int mark { get; set; }
    }

    public class Song
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Artist> artists { get; set; }
        public Album album { get; set; }
        public int duration { get; set; }
        public int copyrightId { get; set; }
        public int status { get; set; }
        public List<object> alias { get; set; }
        public int rtype { get; set; }
        public int ftype { get; set; }
        public int mvid { get; set; }
        public int fee { get; set; }
        public object rUrl { get; set; }
        public int mark { get; set; }
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
