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
        public Water(int x, int y, int width, int height,List<Player> players):base(x,y,width,height)
        {
            ImagePath = "C:/programming/c#/projects/Frog/Frog/Frog/resources/Water.bmp";
            foreach (Player player in players)
            {
                player.ObjectFinishedMove += (PlayableObject item) => { CheckIfCollisionWith(item); };
            }
        }

        public override bool CheckIfCollisionWith(PlayableObject item)
        {
            Player player = item as Player;
            if(!player.IsMoving && !player.IsFlying)
            {
                if (base.CheckIfCollisionWith(item))
                {
                    player.Die();
                    return true;
                }
            }

            return false;
        }
    }
}
