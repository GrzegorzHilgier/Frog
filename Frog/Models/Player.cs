using System;
using System.Windows.Media.Imaging;
using System.Drawing;
using Frog.Utilities;

namespace Frog.Models
{
    class Player: DrawableObject
    {
        private ushort lives;
        public ushort Lives
        {
            get => lives;
            set
            {
                lives = value;
                RaisePropertyChangedEvent("Lives");
            }
        }
        private int score;
        public int Score
        {
            get => score;
            set
            {
                score = value;
                RaisePropertyChangedEvent("Score");
            }
        } 
       


        public Player(ushort lives, int x, int y, int width, int height):base(x,y,width,height)
        {
            Lives = lives;
            Score = 0;
           
            ImagePath = "C:/programming/c#/projects/Frog/Frog/Frog/resources/FrogImg.png";
            

        }
       
    }
}
