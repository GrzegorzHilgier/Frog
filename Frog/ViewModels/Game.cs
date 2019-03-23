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
        Level ActualLevel { get; set; }
        Difficulty ActualDifficulty { get; set; } = Difficulty.EASY;
        private List<Player> players= new List<Player>();
        public ObservableCollection<Player> Players { get; private set; } = new ObservableCollection<Player>();
        public ObservableCollection<PlayableObject> ItemsOnScreen { get; private set; } = new ObservableCollection<PlayableObject>();
       

        public event Action<int> PlayerLostEvent;

        public Game(bool twoPlayers = false)
        {
            //TODO add more players
            Players.Add(new Player("Green",3, mapInfo.Scale *7, mapInfo.Scale *8, mapInfo.Scale -1, mapInfo.Scale -1));
            Players[0].GameOverEvent += (Score) => PlayerLostEvent?.Invoke(Score);

            foreach(Player player in Players)
            {
                players.Add(player);
            }

            ActualLevel = new Level(players, ActualDifficulty, mapInfo, AddItemOnScreen);
            ActualLevel.LevelFinishedEvent += LevelFinished;
        }

        void LevelFinished()
        {
            ItemsOnScreen.Clear();
            ActualLevel.LevelFinishedEvent -= LevelFinished;
            ActualLevel = null;
            GC.Collect();
            GC.WaitForFullGCComplete();
            if(ActualDifficulty == Difficulty.HARD)
            {
                PlayerLostEvent?.Invoke(players[0].Score);
            }
            else
            {
                ActualDifficulty++;
                ActualLevel = new Level(players, ActualDifficulty, mapInfo, AddItemOnScreen);
                ActualLevel.LevelFinishedEvent += LevelFinished;
            }

        }
        public void AddItemOnScreen(PlayableObject item)
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
