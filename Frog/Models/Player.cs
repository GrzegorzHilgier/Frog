using System;
using System.Windows.Media.Imaging;
using System.Drawing;
using Frog.Utilities;

namespace Frog.Models
{
    class Player: DrawableObject
    {
        public ushort Lives { get; private set; }
        public int Score { get; private set; } = 0;
       


        public Player(ushort lives, int x, int y, int width, int height):base(x,y,width,height)
        {
            Lives = lives;
           
            ImagePath = "C:/programming/c#/projects/Frog/Frog/Frog/resources/FrogImg.png";
            

        }
       
    }
}
