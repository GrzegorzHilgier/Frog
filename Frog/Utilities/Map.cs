using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frog.Models
{
    class MapInfo
    {
        public int Scale { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public MapInfo(int scale, ushort height, ushort width)
        {
            Scale = scale;
            Height = scale * height;
            Width = scale * width;
        }
    }
}
