using System;
using Frog.Utilities;

namespace Frog.Models
{
    class Backgroud : DrawableObject
    {   
        public  enum Type{GRASS, ROAD }
        public Backgroud(int x, int y, int width, int height, Type type) : base(x, y, width, height)
        {
            switch(type)
            {
                case Type.GRASS:
                    Image = ResourcesPath.BitmapFromPath(ResourcesPath.GrassPath);
                    break;
                case Type.ROAD:
                    Image = ResourcesPath.BitmapFromPath(ResourcesPath.RoadPath);
                    break;
                default:
                    throw (new ArgumentOutOfRangeException($"Type {type} not recognized"));
                    
            }
        }
        public override void Die()
        {
            IsVisible = false;
        }
    }
}
