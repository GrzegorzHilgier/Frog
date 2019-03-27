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
                    ImagePath = "Grass.png";
                    break;
                case Type.ROAD:
                    ImagePath = "Road.png";
                    break;
                default:
                    throw (new ArgumentOutOfRangeException($"Type {type} not recognized"));
                    
            }
        }
        public override void Die()
        {
           
        }
    }
}
