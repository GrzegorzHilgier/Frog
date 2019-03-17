using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frog.Utilities;

namespace Frog.Models
{
    class Water: DrawableObject
    {
        public Water(int x, int y, int width, int height)
        {
            Xcoord = x;
            Ycoord = y;
            Width = width;
            Height = height;
            ImagePath = "C:/programming/c#/projects/Frog/Frog/Frog/resources/Water.bmp";
        }
    }
}
