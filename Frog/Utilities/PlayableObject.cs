using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Threading;



namespace Frog.Utilities
{
    public enum Direction { LEFT, RIGHT, UP,DOWN };
    abstract class PlayableObject:ObservableObject
    {

        private string imagePath;
        public String ImagePath
        {
            get => imagePath;
            protected set
            {
                imagePath = $"{Directory.GetCurrentDirectory().Replace("\\", "/")}/resources/{value}";
                RaisePropertyChangedEvent("ImagePath");

            }
        }

        private int xcoord;
        public virtual int Xcoord
        {
            get => xcoord;
            set
            {
                    xcoord = value;
                    RaisePropertyChangedEvent("Xcoord");
            }
        }
        private int ycoord;
        public virtual int Ycoord
        {
            get => ycoord;
            set
            {
                ycoord = value;
                RaisePropertyChangedEvent("Ycoord");
            }
        }
        public int Width { get; set; }
        public int Height { get; set; }
        public int StartXcoord { get; set; } = 0;
        public int StartYcoord { get; set; } = 0;

        public PlayableObject( int x, int y, int width, int height)
        {           
            Width = width;
            Height = height;
            Xcoord = x;
            Ycoord = y;
            ImagePath = $"{Directory.GetCurrentDirectory().Replace("\\","/")}/resources/";
        }

        public virtual bool CheckIfCollisionWith(PlayableObject item)
        {
            double opacity = item.Width*0.3;

            if (item.Xcoord + item.Width-opacity >= Xcoord && item.Xcoord + opacity <= Xcoord + Width && item.Ycoord + item.Height - opacity >= Ycoord && item.Ycoord <= Ycoord + Height - opacity)
            {
                return true;
            }
            else return false;
        }

        public abstract void Die();
       

    }
}
