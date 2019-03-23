using System;
using System.Windows.Media.Imaging;
using System.Drawing;
using Frog.Utilities;
using System.Windows.Threading;
using System.Collections.Generic;


namespace Frog.Models
{
    class Player: PlayableObject
    {
        public string Name { get; private set; }

        public event Action<int> GameOverEvent;
        //global variables required for animation
        DispatcherTimer timer = new DispatcherTimer();
        Direction actualDirection;
        int distanceToDestination = 0;
        public bool IsMoving { get; private set; } = false;
        public bool IsFlying { get; set; } = false;

        private ushort lives;
        public ushort Lives
        {
            get => lives;
            set
            {
                lives = value;
                RaisePropertyChangedEvent("Lives");
            }
        }
        private int score;
        public int Score
        {
            get => score;
            set
            {
                score = value;
                RaisePropertyChangedEvent("Score");
            }
        }

        public event Action<PlayableObject> ObjectFinishedMove;
        public event Action<PlayableObject, Direction, Action<bool>> ObjectTryingToMove;
        public event Action<PlayableObject> ObjectMoved;

        public Player(string name, ushort lives, int x, int y, int width, int height):base(x,y,width,height)
        {
            Lives = lives;
            Score = 0;
            Name = name;
            ImagePath = "FrogImg.png";
            timer.Tick += TimerTick;
            timer.Interval = TimeSpan.FromSeconds(0.01);
        }

        public override void Die()
        {
            timer.Stop();
            Lives -= 1;
            if(Lives==0)
            {
                GameOverEvent?.Invoke(Score);
            }
            GoToStartPosition();
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
            if (distanceToDestination > 0)
            {
                switch (actualDirection)
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
