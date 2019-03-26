using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frog.Utilities;
using System.Windows.Threading;

namespace Frog.Models
{
        class Wood : DrawableObject
        {
            private List<Player> Players { get; set; }
            protected DispatcherTimer timer = new DispatcherTimer();
            public int Xmovement { get; private set; }
            public int Ymovement { get; private set; }
            public int MapWidth { get; private set; }
            public Wood(int x, int y, int width, int height, int xmovement, int ymovement, int mapWidth, List<Player> players) : base(x, y, width, height)
            {
                ImagePath = "Wood.png";
                Players = players;
                foreach (Player player in Players)
                {
                    player.Moved +=  CheckIfCollisionWithPlayer; 
                }
                Xmovement = xmovement;
                Ymovement = ymovement;
                MapWidth = mapWidth;
                timer.Tick += MoveOnTick;
                timer.Interval = TimeSpan.FromSeconds(0.02);
                timer.Start();
            }

            public virtual void CheckIfCollisionWithPlayer(DrawableObject item)
            {
                Player player = item as Player;

                if (base.CheckIfCollisionWith(item))
                {
                    if(!player.IsMoving)
                    {
                        player.IsMounted = true;
                        player.Xcoord += Xmovement;
                        player.Ycoord += Ymovement;
                        if(player.Xcoord < -player.Width || player.Xcoord> MapWidth)
                        {
                            player.Die();
                        }
                    }

                }
            }

            protected void MoveOnTick(object sender, EventArgs e)
            {
                Xcoord += Xmovement;
                Ycoord += Ymovement;
                if (Xcoord < -1 * Width) Xcoord = MapWidth;
                if (Xcoord > MapWidth) Xcoord = -1 * Width;
                foreach (Player player in Players)
                {
                    CheckIfCollisionWithPlayer(player);
                }
            }

            public override void Die()
            {
                timer.Tick -= MoveOnTick;
                timer.Stop();
                foreach (Player player in Players)
                {
                    player.Moved -= CheckIfCollisionWithPlayer;
                }

            }
        }
    
}
