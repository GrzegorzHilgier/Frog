using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frog.Utilities;
using System.Windows.Threading;

namespace Frog.Models
{
    class Car :PlayableObject
    {
        private List<Player> Players { get; set; }
        DispatcherTimer timer = new DispatcherTimer();
        public int Xmovement { get; private set; }
        public int Ymovement { get; private set; }
        public int MapWidth { get; private set; }
        public Car(int x, int y, int width, int height, int xmovement, int ymovement, int mapWidth, List<Player>players ) :base(x,y,width,height)
        {
            ImagePath = "Car.bmp";
            Players = players;
            foreach(Player player in Players)
            {
                player.ObjectMoved += (PlayableObject item) => { CheckIfCollisionWith(item); };
            }
            Xmovement = xmovement;
            Ymovement = ymovement;
            MapWidth = mapWidth;
            timer.Tick += TimerTick;
            timer.Interval = TimeSpan.FromSeconds(0.02);
            timer.Start();
        }
        public override bool CheckIfCollisionWith(PlayableObject item)
        {
            Player player = item as Player;

            if (base.CheckIfCollisionWith(item))
            {
                player.Die();
                return true;
            }
            else return false;
        }
        protected override void TimerTick(object sender, EventArgs e)
        {
            Xcoord += Xmovement;
            Ycoord += Ymovement;
            if (Xcoord < -1 * Width) Xcoord = MapWidth;
            if (Xcoord > MapWidth) Xcoord = -1 * Width;
            foreach(Player player in Players)
            {
                CheckIfCollisionWith(player);
            }
        }
    }
}
