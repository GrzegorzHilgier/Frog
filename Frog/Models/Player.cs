using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frog.Models
{
    class Player:ObservableObject
    {
        public Player(ushort lives, int xcoord, int ycoord, int size)
        {
            Lives = lives;
            Xcoord = xcoord;
            Ycoord = ycoord;
            Size = size;
        }

        public ushort Lives { get; private set; }
        public int Score { get; private set; } = 0;
        private int xcoord;
        public int Xcoord
        {
            get => xcoord;
            set
            {
                xcoord = value;
                RaisePropertyChangedEvent("Xcoord");
            }
        }
        private int ycoord;
        public int Ycoord
        {
            get => ycoord;
            set
            {
                ycoord = value;
                RaisePropertyChangedEvent("Ycoord");
            }
        }
        public int Size { get; private set; }
    }
}
