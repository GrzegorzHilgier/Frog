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

        public WoodSunk(int x, int y, int width, int height, int xmovement, int ymovement, int mapWidth, List<Player> players) : base(x, y, width, height,xmovement,ymovement,mapWidth,players)
        {
            timer.Tick += SinkOnTick;
        }

        public override void CheckIfCollisionWithPlayer(PlayableObject item)
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
                        player.IsFlying = true;
                        player.Xcoord += Xmovement;
                        player.Ycoord += Ymovement;
                        if (player.Xcoord < -player.Width || player.Xcoord > MapWidth)
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
                ImagePath = "Wood.bmp";
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
                ImagePath = "Wood.bmp";
            }
            
        }
        public override void Die()
        {
            base.Die();

        }
    }

}

