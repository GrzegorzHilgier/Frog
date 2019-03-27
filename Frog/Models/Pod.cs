using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frog.Utilities;


namespace Frog.Models
{
    class Pod : DrawableObject
    {
        static ushort CreatedPods = 0;
        static ushort OccupiedPods = 0;
        private List<Player> Players { get; set; }
        public bool IsChecked { get; private set; } = false;

        public static event Action AllPodsOccupied;
        public static event Action<Player> PlayerScored;

        public Pod(int x, int y, int width, int height, List<Player> players):base(x,y,width,height)
        {
            CreatedPods ++;
            ImagePath = "PodEmpty.png";
            Players = players;
            foreach(Player player in Players)
            {
                player.FinishedMove +=  CheckIfCollisionWithPlayer;
                player.TryingToMove += CheckIfPlayerCanGetIn;

            }
        }
        public void CheckIfCollisionWithPlayer(DrawableObject item)
        {
            Player player = item as Player;
            if(!IsChecked)
            {
                if (base.CheckIfCollisionWith(item))
                {
                    IsChecked = true;
                    ImagePath = "PodOccupied.png";
                    OccupiedPods++;
                    PlayerScored?.Invoke(player);
                    if (OccupiedPods == CreatedPods)
                    {
                        AllPodsOccupied?.Invoke();
                    }
                }               
            }
        }
        public override void Die()
        {
            foreach (Player player in Players)
            {
                player.FinishedMove -= CheckIfCollisionWithPlayer;
                player.TryingToMove -= CheckIfPlayerCanGetIn;
            }

        }

        public void CheckIfPlayerCanGetIn(DrawableObject item, Direction direction, Action<bool> action)
        {
            switch(direction)
            {
                case Direction.LEFT:
                    if (item.Ycoord + item.Height >= Ycoord && item.Ycoord < Ycoord + Height)
                        action(false);
                    else action(true);
                    break;
                case Direction.RIGHT:
                    if (item.Ycoord + item.Height >= Ycoord && item.Ycoord < Ycoord + Height)
                        action(false);
                    else action(true);
                    action(true);
                    break;
                default:
                    action(true);
                    break;
            }
        }
        

    }
}
