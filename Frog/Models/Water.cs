using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frog.Utilities;
using Frog.Models;


namespace Frog.Models
{
    class Water: PlayableObject
    {
        private List<Player> Players { get; set; }
        public Water(int x, int y, int width, int height,List<Player> players):base(x,y,width,height)
        {
            Players = players;
            ImagePath = "Water.bmp";
            foreach (Player player in players)
            {
                player.ObjectFinishedMove += CheckIfCollisionWithPlayer; 
            }
        }
      
        public void CheckIfCollisionWithPlayer(PlayableObject item)
        {
            Player player = item as Player;
            if(!player.IsMoving && !player.IsFlying)
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
                player.ObjectFinishedMove -= CheckIfCollisionWithPlayer;
            }
            Players = null;
            base.Die();
            
        }
    }
}
