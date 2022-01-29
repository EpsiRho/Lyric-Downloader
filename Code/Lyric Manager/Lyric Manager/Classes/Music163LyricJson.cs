using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyric_Manager.Classes
{
    public class TransUser
    {
        public int id { get; set; }
        public int status { get; set; }
        public int demand { get; set; }
        public int userid { get; set; }
        public string nickname { get; set; }
        public long uptime { get; set; }
    }

    public class LyricUser
    {
        public int id { get; set; }
        public int status { get; set; }
        public int demand { get; set; }
        public int userid { get; set; }
        public string nickname { get; set; }
        public long uptime { get; set; }
    }

    public class Lrc
    {
        public int version { get; set; }
        public string lyric { get; set; }
    }

    public class Klyric
    {
        public int version { get; set; }
        public string lyric { get; set; }
    }

    public class Tlyric
    {
        public int version { get; set; }
        public string lyric { get; set; }
    }

    public class Music163LyricJson
    {
        public bool sgc { get; set; }
        public bool sfy { get; set; }
        public bool qfy { get; set; }
        public TransUser transUser { get; set; }
        public LyricUser lyricUser { get; set; }
        public Lrc lrc { get; set; }
        public Klyric klyric { get; set; }
        public Tlyric tlyric { get; set; }
        public int code { get; set; }
    }
}
