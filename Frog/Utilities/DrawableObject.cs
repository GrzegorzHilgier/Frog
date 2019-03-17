using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Drawing;



namespace Frog.Utilities
{
    class DrawableObject:ObservableObject
    {
        public String ImagePath { get; protected set; }

        private int xcoord;
        public virtual int Xcoord
        {
            get => xcoord;
            set
            {
                if(CheckIfXMovePossible(value))
                {
                    xcoord = value;
                    RaisePropertyChangedEvent("Xcoord");
                }
            }
        }
        private int ycoord;
        public virtual int Ycoord
        {
            get => ycoord;
            set
            {
                if (CheckIfYMovePossible(value))
                {
                    ycoord = value;
                    RaisePropertyChangedEvent("Ycoord");
                }
            }
        }
        public int Width { get; set; }
        public int Height { get; set; }
        //public BitmapImage Image { get; set; }

        public virtual bool CheckIfXMovePossible(int value)
        {
            return true;
        }
        public virtual bool CheckIfYMovePossible(int value)
        {
            return true;
        }

    }
}
