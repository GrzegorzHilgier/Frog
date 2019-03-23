using System;
using System.Windows.Media.Imaging;
using System.Drawing;
using Frog.Utilities;


namespace Frog.Models
{
    class Player: DrawableObject
    {
        public string Name { get; private set; }
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
       


        public Player(string name, ushort lives, int x, int y, int width, int height):base(x,y,width,height)
        {
            Lives = lives;
            Score = 0;
            Name = name;
            ImagePath = "C:/programming/c#/projects/Frog/Frog/Frog/resources/FrogImg.png";


        }
        public void Die()
        {
            Lives -= 1;
            GoToStartPosition();
        }
       
    }
}
