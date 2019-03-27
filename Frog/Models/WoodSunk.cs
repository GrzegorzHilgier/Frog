using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frog.Utilities;
using System.Windows.Threading;

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
                ImagePath = string.Empty;
                IsUnderwater = true;
            }
            else if (TickCounter == 300)
            {
                ImagePath = "Wood.png";
                IsUnderwater = false;
                TickCounter = 0;
            }
        }

        public void Blink(object sender, EventArgs e)
        {
            if(TickCounter % 20 == 0)
            {
                ImagePath = string.Empty;
            }
            else if(TickCounter % 10 == 0)
            {
                ImagePath = "Wood.png";
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

