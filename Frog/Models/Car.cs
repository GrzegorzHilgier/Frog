using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frog.Utilities;

namespace Frog.Models
{
    class Car :DrawableObject
    {
        private List<Player> Players { get; set; }
        public Car(int x, int y, int width, int height,List<Player>players ) :base(x,y,width,height)
        {
            ImagePath = "C:/programming/c#/projects/Frog/Frog/Frog/resources/Car.bmp";
            Players = players;
            foreach(Player player in Players)
            {
                player.ObjectMoved += CheckIfCollisionWith;
            }

        }
        public override void CheckIfCollisionWith(DrawableObject item)
        {
            Player player = item as Player;

                if (item.Xcoord+item.Width >= Xcoord && item.Xcoord <= Xcoord + Width && item.Ycoord + item.Height >= Ycoord && item.Ycoord <= Ycoord + Height)
                {
                     player.Lives -= 1;
                     player.GoToStartPosition();  
                }
            
        }
    }
}
