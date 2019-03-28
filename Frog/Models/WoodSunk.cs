using System;
using System.Collections.Generic;
using Frog.Utilities;

namespace Frog.Models
{
    class WoodSunk : Wood
    {
        private bool IsUnderwater = false;
        private int TickCounter = 0;

        public WoodSunk(int x, int y, int width, int height, int xmovement, int ymovement, MapInfo mapInfo, List<Player> players): base(x, y, width, height, xmovement, ymovement, mapInfo, players)
        {           
            timer.Tick += SinkOnTick;
        }

        public override void CheckIfCollisionWithPlayer(DrawableObject item)
        {
            Player player = item as Player;

            if (base.CheckIfCollisionWith(item))
            {
                if (!player.IsMoving)
                {
                    if(IsUnderwater)
                    {
                        player.Die();
                    }
                    else
                    {
                        player.IsMounted = true;
                        player.Xcoord += Xmovement;
                        player.Ycoord += Ymovement;
                        if (player.Xcoord < -player.Width || player.Xcoord > mapInfo.Width)
                        {
                            player.Die();
                        }
                    }

                }

            }
        }

    
        private void SinkOnTick(object sender, EventArgs e)
        {
            TickCounter++;
            if (TickCounter == 100)
            {
                 timer.Tick += Blink;            
            }
            else if(TickCounter ==200)
            {
                timer.Tick -= Blink;
                IsVisible = false;
                IsUnderwater = true;
            }
            else if (TickCounter == 300)
            {
                IsVisible = true;
                IsUnderwater = false;
                TickCounter = 0;
            }
        }

        public void Blink(object sender, EventArgs e)
        {
            if(TickCounter % 20 == 0)
            {
                IsVisible = false;
            }
            else if(TickCounter % 10 == 0)
            {
                IsVisible = true ;
            }
            
        }

        public override void Die()
        {
            timer.Tick -= Blink;
            timer.Tick -= SinkOnTick;
            base.Die();
        }
    }

}

