﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frog.Utilities;


namespace Frog.Models
{
    class Pod : PlayableObject
    {
        static ushort CreatedPods = 0;
        static ushort OccupiedPods = 0;
        private List<Player> Players { get; set; }
        public bool IsChecked { get; private set; } = false;

        public static event Action AllPodsOccupied;

        public Pod(int x, int y, int width, int height, List<Player> players):base(x,y,width,height)
        {
            CreatedPods ++;
            ImagePath = "PodEmpty.bmp";
            Players = players;
            foreach(Player player in Players)
            {
                player.ObjectMoved +=  CheckIfCollisionWithPlayer;
                player.ObjectTryingToMove += CheckIfPlayerCanGetIn;

            }
        }
        public void CheckIfCollisionWithPlayer(PlayableObject item)
        {
            Player player = item as Player;
            if(!IsChecked)
            {
                if (base.CheckIfCollisionWith(item))
                {
                    IsChecked = true;
                    ImagePath = "PodOccupied.bmp";
                    OccupiedPods++;
                    player.Score += 100;
                    if (OccupiedPods == CreatedPods)
                    {
                        AllPodsOccupied?.Invoke();
                    }
                    else
                    {                       
                        player.GoToStartPosition();
                    }
                }               
            }
        }
        public override void Die()
        {
            foreach (Player player in Players)
            {
                player.ObjectFinishedMove -= CheckIfCollisionWithPlayer;
                player.ObjectTryingToMove -= CheckIfPlayerCanGetIn;
            }
            Players.Clear();

        }

        public void CheckIfPlayerCanGetIn(PlayableObject item, Direction direction, Action<bool> action)
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
