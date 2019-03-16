using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;



namespace Frog.Utilities
{
    class DrawableObject:ObservableObject
    {   private int xcoord;
        public int Xcoord
        {
            get => xcoord;
            set
            {
                xcoord = value;
                RaisePropertyChangedEvent("Xcoord");

            }
        }
        private int ycoord;
        public int Ycoord
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
        //public BitmapImage Image { get; set; }
    }
}
