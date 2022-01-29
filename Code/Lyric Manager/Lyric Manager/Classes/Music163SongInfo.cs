using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyric_Manager.Classes
{
    public class HMusic
    {
        public object name { get; set; }
        public long id { get; set; }
        public int size { get; set; }
        public string extension { get; set; }
        public int sr { get; set; }
        public int dfsId { get; set; }
        public int bitrate { get; set; }
        public int playTime { get; set; }
        public double volumeDelta { get; set; }
    }

    public class MMusic
    {
        public object name { get; set; }
        public long id { get; set; }
        public int size { get; set; }
        public string extension { get; set; }
        public int sr { get; set; }
        public int dfsId { get; set; }
        public int bitrate { get; set; }
        public int playTime { get; set; }
        public double volumeDelta { get; set; }
    }

    public class LMusic
    {
        public object name { get; set; }
        public long id { get; set; }
        public int size { get; set; }
        public string extension { get; set; }
        public int sr { get; set; }
        public int dfsId { get; set; }
        public int bitrate { get; set; }
        public int playTime { get; set; }
        public double volumeDelta { get; set; }
    }

    public class BMusic
    {
        public object name { get; set; }
        public long id { get; set; }
        public int size { get; set; }
        public string extension { get; set; }
        public int sr { get; set; }
        public int dfsId { get; set; }
        public int bitrate { get; set; }
        public int playTime { get; set; }
        public double volumeDelta { get; set; }
    }

    public class Equalizers
    {
    }

    public class Music163SongInfo
    {
        public List<Song> songs { get; set; }
        public Equalizers equalizers { get; set; }
        public int code { get; set; }
    }

}
