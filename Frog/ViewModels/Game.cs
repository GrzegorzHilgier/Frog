using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Frog.Models;
using Frog.Utilities;


namespace Frog.ViewModels
{
    class Game : ObservableObject
    {
        private MapInfo mapInfo = new MapInfo(30, 9, 15);
        private Queue<Level> LevelQueue { get; set; } = new Queue<Level>();
        private Level ActualLevel { get; set; }

        
        private List<Player> players= new List<Player>();

        private int levelsLeft;
        public int LevelsLeft
        {
            get => levelsLeft;
            private set
            {
                levelsLeft = value;
                RaisePropertyChangedEvent(nameof(LevelsLeft));
            }
        }

        private int levelTime;
        public int LevelTime
        {
            get => levelTime;
            private set
            {
                levelTime = value;
                RaisePropertyChangedEvent(nameof(LevelTime));
            }
                
        }
        public ObservableCollection<Player> Players { get; private set; } = new ObservableCollection<Player>();
        public ObservableCollection<DrawableObject> ItemsOnScreen { get; private set; } = new ObservableCollection<DrawableObject>();
       
        public event Action GameOver;


        public void Start(bool twoPlayers = false)
        {
            
            //TODO add more players
            Players.Add(new Player("Green", 3, mapInfo.Scale * 7, mapInfo.Scale * 8, mapInfo.Scale - 1, mapInfo.Scale - 1));

            foreach (Player player in Players)
            {
                players.Add(player);
            }

            LevelFactory.LoadLevels(LevelQueue);
            ActualLevel = LevelQueue.Dequeue();
            LevelsLeft = LevelQueue.Count;
            Init(ActualLevel);
        }

        public void AddItemOnScreen(DrawableObject item)
        {
            ItemsOnScreen.Add(item);
        }

        public void Clear()
        {
            Players.Clear();
            players.Clear();
            ItemsOnScreen.Clear();
            LevelQueue.Clear();
        }

        private void Init(Level level)
        {
            level.Init(players, mapInfo, AddItemOnScreen);
            level.LevelFinishedEvent += LevelFinished;
            level.LevelTimeChanged += LevelTimerTick;
        }
        private void Clear(Level level)
        {
            level.LevelFinishedEvent -= LevelFinished;
            level.LevelTimeChanged -= LevelTimerTick;
        }
        private void LevelTimerTick()
        {
            LevelTime = ActualLevel.LevelTime;
        }

        private void LevelFinished(bool PlayerWon)
        {
            ItemsOnScreen.Clear();
            Clear(ActualLevel);

            if (PlayerWon)
            {
                if (LevelQueue.Count==0)
                {
                    GameOver?.Invoke();
                }
                else
                {
                    ActualLevel = LevelQueue.Dequeue();
                    Init(ActualLevel);
                    LevelsLeft = LevelQueue.Count;
                }
            }
            else
            {
                GameOver?.Invoke();
            }
        }

        public ICommand MoveLeftCommand
        {
            get { return new SimpleCommand(MoveLeft); }
        }
        public ICommand MoveRightCommand
        {
            get { return new SimpleCommand(MoveRight); }
        }
        public ICommand MoveUpCommand
        {
            get { return new SimpleCommand(MoveUp); }
        }
        public ICommand MoveDownCommand
        {
            get { return new SimpleCommand(MoveDown); }
        }

        private void MoveLeft()
        {
            if(Players[0].Xcoord > Players[0].Width)
            {
                Players[0].TryToMove(Direction.LEFT, mapInfo.Scale);
            }
        }

        private void MoveRight()
        {
            if(Players[0].Xcoord < mapInfo.Width-mapInfo.Scale)
            {
                Players[0].TryToMove(Direction.RIGHT, mapInfo.Scale);
            }

        }

        private void MoveUp()
        {
            if(Players[0].Ycoord >= Players[0].Height)
            {
                Players[0].TryToMove(Direction.UP, mapInfo.Scale);
            }

        }

        private void MoveDown()
        {
            if (Players[0].Ycoord < mapInfo.Height- mapInfo.Scale)
            {
                Players[0].TryToMove(Direction.DOWN, mapInfo.Scale);
            }
        }
    }
}
