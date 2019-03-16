using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frog.Utilities;

namespace Frog.Models
{
    class Player: DrawableObject
    {
        public ushort Lives { get; private set; }
        public int Score { get; private set; } = 0;

        public Player(ushort lives, int xcoord, int ycoord, int size)
        {
            Lives = lives;
            Xcoord = xcoord;
            Ycoord = ycoord; 
        }
    }
}
