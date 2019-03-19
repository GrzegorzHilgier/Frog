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
    public enum Direction { LEFT, RIGHT, UP,DOWN };
    class DrawableObject:ObservableObject
    {
 
        private String imagePath;
        public String ImagePath
        {
            get => imagePath;
            protected set
            {
                imagePath = value;
                RaisePropertyChangedEvent("ImagePath");

            }
        }

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

        public DrawableObject( int x, int y, int width, int height)
        {           
            Width = width;
            Height = height;
            Xcoord = x;
            Ycoord = y;
            StartXcoord = x;
            StartYcoord = y;
        }

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
        public int StartXcoord { get; set; } = 0;
        public int StartYcoord { get; set; } = 0;
        //public BitmapImage Image { get; set; }


        public virtual bool CheckIfXMovePossible(int value)
        {
            return true;
        }
        public virtual bool CheckIfYMovePossible(int value)
        {
            return true;
        }
        public virtual void CheckIfCollisionWith(DrawableObject item) { }

        public event Action<DrawableObject> ObjectMoved;
        public event Action<DrawableObject, Direction, Action<bool>> ObjectTryingToMove;
        public void RaiseObjectMovedEvent()
        {
            ObjectMoved(this);
        }
        public void TryToMove(Direction direction, int value)
        {
            bool finalResult = true;
            //checks if any subscriber will block movement
            var results = new List<bool>();
            ObjectTryingToMove?.Invoke(this, direction, val => results.Add(val));
            foreach (bool result in results)
            {
                if (!result)
                {
                    finalResult = false;
                    break;
                }

            }
            if (finalResult)
            {
                switch(direction)
                {
                    case Direction.DOWN:
                        Ycoord += value;
                            break;
                    case Direction.UP:
                        Ycoord -= value;
                        break;
                    case Direction.LEFT:
                        Xcoord -= value;
                        break;
                    case Direction.RIGHT:
                        Xcoord += value;
                        break;
                }

            }

        }

        public void GoToStartPosition()
        {
            Xcoord = StartXcoord;
            Ycoord = StartYcoord;
        }

    }
}
