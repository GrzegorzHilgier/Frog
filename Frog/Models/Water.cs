﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frog.Utilities;
using Frog.Models;


namespace Frog.Models
{
    class Water: DrawableObject
    {
        private List<Player> Players { get; set; }
        public Water(int x, int y, int width, int height,List<Player> players):base(x,y,width,height)
        {
            Players = players;
            ImagePath = "Water.bmp";
            foreach (Player player in players)
            {
                player.FinishedMove += CheckIfCollisionWithPlayer; 
            }
        }
      
        public void CheckIfCollisionWithPlayer(DrawableObject item)
        {
            Player player = item as Player;
            if(!player.IsMoving && !player.IsMounted)
            {
                if (base.CheckIfCollisionWith(item))
                {
                    player.Die();
                }
            }
        }
        public override void Die()
        {
            foreach (Player player in Players)
            {
                player.FinishedMove -= CheckIfCollisionWithPlayer;
            }
            
        }
    }
}
