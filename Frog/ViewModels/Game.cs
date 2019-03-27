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
        MapInfo mapInfo = new MapInfo(30, 9, 15);
        List<Level> LevelList { get; set; } = new List<Level>();
        
        private List<Player> players= new List<Player>();

        private int levelTime;
        public int LevelTime
        {
            get => levelTime;
            private set
            {
                levelTime = value;
                RaisePropertyChangedEvent("LevelTime");
            }
                
        }
        public ObservableCollection<Player> Players { get; private set; } = new ObservableCollection<Player>();
        public ObservableCollection<DrawableObject> ItemsOnScreen { get; private set; } = new ObservableCollection<DrawableObject>();
       
        public event Action GameOver;

        public Game(bool twoPlayers = false)
        {
            //TODO add more players
            Players.Add(new Player("Green",3, mapInfo.Scale *7, mapInfo.Scale *8, mapInfo.Scale -1, mapInfo.Scale -1));

            foreach(Player player in Players)
            {
                players.Add(player);
            }

            LevelFactory.LoadLevels(LevelList);
            LevelList[0].Init(players, mapInfo, AddItemOnScreen);
            LevelList[0].LevelFinishedEvent += LevelFinished;
            LevelList[0].LevelTimeChangedEvent += LevelTimerTick;
        }

        void LevelTimerTick()
        {
            LevelTime = LevelList[0].LevelTime;
        }

        void LevelFinished(bool PlayerWon)
        {

            if(PlayerWon)
            {
                ItemsOnScreen.Clear();
                LevelList[0].LevelFinishedEvent -= LevelFinished;
                LevelList[0].LevelTimeChangedEvent -= LevelTimerTick;
                LevelList.RemoveAt(0);
                GC.Collect();
                GC.WaitForFullGCComplete();
                if (LevelList.Count==0)
                {
                    GameOver?.Invoke();
                }
                else
                {
                    LevelList[0].Init(players, mapInfo, AddItemOnScreen);
                    LevelList[0].LevelFinishedEvent += LevelFinished;
                    LevelList[0].LevelTimeChangedEvent += LevelTimerTick;
                }
            }
            else
            {
                GameOver?.Invoke();
            }
        }
        public void AddItemOnScreen(DrawableObject item)
        {
            ItemsOnScreen.Add(item);
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

        void MoveLeft()
        {
            if(Players[0].Xcoord > Players[0].Width)
            {
                Players[0].TryToMove(Direction.LEFT, mapInfo.Scale);
            }
        }

        void MoveRight()
        {
            if(Players[0].Xcoord < mapInfo.Width-mapInfo.Scale)
            {
                Players[0].TryToMove(Direction.RIGHT, mapInfo.Scale);
            }

        }

        void MoveUp()
        {
            if(Players[0].Ycoord >= Players[0].Height)
            {
                Players[0].TryToMove(Direction.UP, mapInfo.Scale);
            }

        }

        void MoveDown()
        {
            if (Players[0].Ycoord < mapInfo.Height- mapInfo.Scale)
            {
                Players[0].TryToMove(Direction.DOWN, mapInfo.Scale);
            }
        }
    }
}
