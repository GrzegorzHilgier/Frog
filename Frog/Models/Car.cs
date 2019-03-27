﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frog.Utilities;
using System.Windows.Threading;

namespace Frog.Models
{
    class Car :DrawableObject
    {
        protected MapInfo mapInfo;
        private List<Player> Players { get; set; }
        DispatcherTimer timer = new DispatcherTimer();
        public int Xmovement { get; private set; }
        public int Ymovement { get; private set; }
 
        
        public Car(int x, int y, int width, int height, int xmovement, int ymovement, MapInfo mapInfo, List<Player>players ) :base(x,y,width,height)
        {
            ImagePath = "Car.png";
            Players = players;
            foreach(Player player in Players)
            {
                player.Moved += CheckIfCollisionWithPlayer;
            }
            Xmovement = xmovement;
            Ymovement = ymovement;
            this.mapInfo = mapInfo;
            timer.Tick += TimerTick;
            timer.Interval = TimeSpan.FromSeconds(0.02);
            timer.Start();
        }
        public void CheckIfCollisionWithPlayer(DrawableObject item)
        {
            Player player = item as Player;

            if (base.CheckIfCollisionWith(item))
            {
                player.Die();
            }
        }
        protected  void TimerTick(object sender, EventArgs e)
        {
            Xcoord += Xmovement;
            Ycoord += Ymovement;
            if (Xcoord < -mapInfo.MaxObjectWidth) Xcoord = mapInfo.Width;
            if (Xcoord > mapInfo.Width) Xcoord = -mapInfo.MaxObjectWidth;
            foreach(Player player in Players)
            {
                CheckIfCollisionWithPlayer(player);
            }
        }
        public override void Die()
        {
            timer.Tick -= TimerTick;
            timer.Stop();
            foreach (Player player in Players)
            {
                player.Moved -= CheckIfCollisionWithPlayer;
            }

        }
    }
}
