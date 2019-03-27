using System;
using System.Windows.Media.Imaging;
using System.Drawing;
using Frog.Utilities;
using System.Windows.Threading;
using System.Collections.Generic;


namespace Frog.Models
{
    class Player: DrawableObject
    {
        public static int PlayersInGame = 0;

        DispatcherTimer timer = new DispatcherTimer();

        Direction actualDirection;
        int distanceToDestination = 0;

        public bool IsMoving { get; private set; } = false;
        public bool IsMounted { get; set; } = false;

        private int lives;
        public int Lives
        {
            get => lives;
            private set
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

        public string Name { get; private set; }

        public event Action<Player> LostLife;
        public event Action<Player> OutOfLives;

        public event Action<DrawableObject, Direction, Action<bool>> TryingToMove;
        public event Action<DrawableObject> Moved;
        public event Action<DrawableObject> FinishedMove;

        public Player(string name, ushort lives, int x, int y, int width, int height):base(x,y,width,height)
        {
            Lives = lives;
            PlayersInGame++;
            Score = 0;
            Name = name;
            ImagePath = "FrogImg.png";
            timer.Tick += TimerTick;
            StartXcoord = x;
            StartYcoord = y;
            timer.Interval = TimeSpan.FromSeconds(0.02);
        }

        public override void Die()
        {
            Lives -= 1;
            if (Lives>0)
            {
                LostLife?.Invoke(this);
            }
            else
            {
                PlayersInGame--;
                timer.Tick -= TimerTick;
                ImagePath = string.Empty;
                OutOfLives?.Invoke(this);
            }
            
        }

        public void TryToMove(Direction direction, int value)
        {

                if (IsMoving) return;
                bool finalResult = true;
                //checks if any subscriber will block movement
                var results = new List<bool>();
                TryingToMove?.Invoke(this, direction, val => results.Add(val));
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
            IsMounted = false;
            actualDirection = direction;
            distanceToDestination = value;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            ushort stepDistance = 10;
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
                Moved?.Invoke(this);
            }
            else
            {
                timer.Stop();
                IsMoving = false;
                Moved?.Invoke(this);
                FinishedMove?.Invoke(this);
            }
        }

        public void GoToStartPosition()
        {
            timer.Stop();
            IsMoving = false;
            IsMounted = false;
            Xcoord = StartXcoord;
            Ycoord = StartYcoord;
        }

    }
}
