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
        public Pod(int x, int y, int width, int height)
        {
            Xcoord = x;
            Ycoord = y;
            Width = width;
            Height = height;
            ImagePath = "C:/programming/c#/projects/Frog/Frog/Frog/resources/PodEmpty.bmp";
        }
        public override void CheckIfOn(DrawableObject item)
        {
            if (item.Xcoord >= Xcoord && item.Xcoord <= Xcoord + Width && item.Ycoord >= Ycoord && item.Ycoord <= Ycoord + Height)
            {
                ImagePath = "C:/programming/c#/projects/Frog/Frog/Frog/resources/PodOccupied.bmp";
                RaisePropertyChangedEvent("ImagePath");
            }

        }

    }
}
