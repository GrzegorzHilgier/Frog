﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frog.Utilities;


namespace Frog.Models
{
    class Pod : DrawableObject
    {
        private List<Player> Players { get; set; }
        public bool IsChecked { get; private set; } = false;
        public Pod(int x, int y, int width, int height, List<Player> players):base(x,y,width,height)
        {
            ImagePath = "C:/programming/c#/projects/Frog/Frog/Frog/resources/PodEmpty.bmp";
            Players = players;
            foreach(Player player in Players)
            {
                player.ObjectMoved += CheckIfCollisionWith;
            }
        }
        public override void CheckIfCollisionWith(DrawableObject item)
        {
            Player player = item as Player;
            if(!IsChecked)
            {
                if (item.Xcoord >= Xcoord && item.Xcoord < Xcoord + Width && item.Ycoord >= Ycoord && item.Ycoord < Ycoord + Height)
                {
                    IsChecked = true;
                    ImagePath = "C:/programming/c#/projects/Frog/Frog/Frog/resources/PodOccupied.bmp";
                    RaisePropertyChangedEvent("ImagePath");
                    player.GoToStartPosition();
                }
            }
        }

    }
}
