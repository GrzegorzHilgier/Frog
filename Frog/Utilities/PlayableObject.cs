using System;
using System.Collections.Generic;
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
    class PlayableObject:ObservableObject
    {

        //global variables required for animation
        DispatcherTimer timer = new DispatcherTimer();
        Direction actualDirection;
        int distanceToDestination=0;       
        public bool IsMoving { get; private set; } = false;
        public bool IsFlying { get; set; } = false;

        private string imagePath;
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

        public event Action<PlayableObject> ObjectMoved;
        public event Action<PlayableObject> ObjectFinishedMove;
        public event Action<PlayableObject, Direction, Action<bool>> ObjectTryingToMove;

        public PlayableObject( int x, int y, int width, int height)
        {           
            Width = width;
            Height = height;
            Xcoord = x;
            Ycoord = y;
            StartXcoord = x;
            StartYcoord = y;
            timer.Tick += TimerTick;
            timer.Interval = TimeSpan.FromSeconds(0.01);
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

        public void TryToMove(Direction direction, int value)
        {
            if (IsMoving) return;
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
                GoToPosition(direction, value);
            }

        }
        private void GoToPosition(Direction direction, int value)
        {
            IsMoving = true;
            IsFlying = false;
            actualDirection = direction;
            distanceToDestination = value;
            timer.Start();
        }

        protected virtual void TimerTick(object sender, EventArgs e)
        {
            ushort stepDistance = 5;
            if(distanceToDestination>0)
            {
                switch(actualDirection)
                {
                    case Direction.DOWN:
                        Ycoord += stepDistance;
                        break;
                    case Direction.UP:
                        Ycoord -= stepDistance;
                        break;
                    case Direction.LEFT:
                        Xcoord -= stepDistance;
                        break;
                    case Direction.RIGHT:
                        Xcoord += stepDistance;
                        break;
                }
                distanceToDestination -= stepDistance;
                ObjectMoved?.Invoke(this);
            }
            else
            {
                timer.Stop();
                IsMoving = false;
                ObjectMoved?.Invoke(this);
                ObjectFinishedMove?.Invoke(this);
            }
        }

        public void GoToStartPosition()
        {
            timer.Stop();
            IsMoving = false;
            IsFlying = false;
            Xcoord = StartXcoord;
            Ycoord = StartYcoord;
        }

    }
}
