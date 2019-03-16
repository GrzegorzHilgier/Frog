using System;
using System.Windows.Media.Imaging;
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
            Width = 30;
            Height = 30;
            //Uri uri = new Uri("resources/Frog.png");
            //Image = new BitmapImage(uri);
        }
    }
}
