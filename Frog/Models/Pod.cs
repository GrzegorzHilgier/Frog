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
        static event Action AllPodsOccupied;
        public Pod(int x, int y, int width, int height, List<Player> players):base(x,y,width,height)
        {
            CreatedPods ++;
            ImagePath = "C:/programming/c#/projects/Frog/Frog/Frog/resources/PodEmpty.bmp";
            Players = players;
            foreach(Player player in Players)
            {
                player.ObjectMoved += CheckIfCollisionWith;
                player.ObjectTryingToMove += CheckIfPlayerCanGetIn;
               // player.ObjectTryingToMove+=
            }
        }
        public override void CheckIfCollisionWith(DrawableObject item)
        {
            Player player = item as Player;
            if(!IsChecked)
            {
                if (item.Xcoord >= Xcoord && item.Xcoord < Xcoord + Width && item.Ycoord >= Ycoord && item.Ycoord <= Ycoord + Height)
                {
                    IsChecked = true;
                    ImagePath = "C:/programming/c#/projects/Frog/Frog/Frog/resources/PodOccupied.bmp";
                    OccupiedPods++;
                    RaisePropertyChangedEvent("ImagePath");
                    if (OccupiedPods == CreatedPods)
                    {
                        AllPodsOccupied?.Invoke();
                    }
                    else
                    {
                        player.Score += 100;
                        player.GoToStartPosition();

                    }
                }
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
