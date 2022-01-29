using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyric_Manager.Classes
{
    public static class ProcessHandler
    {
        public static Process proc { get; set; }
        public static IntPtr hwnd { get; set; }
    }
}
