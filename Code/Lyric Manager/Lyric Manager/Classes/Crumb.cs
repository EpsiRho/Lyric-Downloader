using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyric_Manager.Classes
{
    public readonly struct Crumb
    {
        public Crumb(string label, object data)
        {
            Label = label;
            Data = data;
        }
        public string Label { get; }
        public object Data { get; }
        public override string ToString() => Label;
    }
}
