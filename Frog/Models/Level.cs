using System;
using System.Collections.Generic;
using Frog.Utilities;
using System.Windows.Threading;

namespace Frog.Models
{
    abstract class Level
    {

        private DispatcherTimer LevelTimer { get; set; } = new DispatcherTimer();
        private List<DrawableObject> ItemsInGame { get; set; } = new List<DrawableObject>();
        private Action<DrawableObject> AddItemToGame { get; set; }

        protected List<Player> Players { get; set; }

        protected virtual int LevelMaxTime { get; set; } = 20;
        private int levelTime;
        public int LevelTime
        {
            get=>levelTime;
            protected set
            {
                levelTime = value;
                LevelTimeChanged?.Invoke();
            }
        } 

        public event Action LevelTimeChanged;
        public event Action<bool> LevelFinishedEvent;
        //True => player won, false => player lost

        private void PlayerLostLife(Player player)
        {
            player.GoToStartPosition();
            ResetTime();
        }

        private void PlayerGameOver(Player player)
        {
            if (Player.PlayersInGame == 0)
            {
                RaiseLevelFinishedEvent(false);
            }

        }

        protected  void RaiseLevelFinishedEvent(bool PlayerWon)
        {
            Clean();
            foreach (DrawableObject item in ItemsInGame)
            {
                item.Die();
            }

            foreach (Player player in Players)
            {
                player.OutOfLives -= PlayerGameOver;
                player.LostLife -= PlayerLostLife;
            }
            LevelTimer.Tick -= TimerTick;
            LevelTimer.Stop();
            ItemsInGame.Clear();
            AddItemToGame = null;

            LevelFinishedEvent?.Invoke(PlayerWon);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            LevelTime--;
            if(LevelTime<0)
            {
                foreach (Player player in Players)
                {
                    player.Die();
                }              
            }
            
        }

        protected void ResetTime()
        {
            levelTime = LevelMaxTime;
        }

        protected virtual void GivePoints(Player player)
        {
            player.Score += LevelTime * 10;
            player.GoToStartPosition();
            ResetTime();
        }

        protected void AddItem(DrawableObject item)
        {
            ItemsInGame.Add(item);
            AddItemToGame(item);
        }

        protected abstract void Clean();

        public virtual void Init(List<Player> players, MapInfo mapInfo, Action<DrawableObject> addItemToGame)
        {

            AddItemToGame += addItemToGame;
            Players = players;
            LevelTime = LevelMaxTime;

            foreach (Player player in Players)
            {
                player.GoToStartPosition();
                player.OutOfLives += PlayerGameOver;
                player.LostLife += PlayerLostLife;
            }

            LevelTimer = new DispatcherTimer();
            LevelTimer.Interval = TimeSpan.FromSeconds(1);
            LevelTimer.Tick += TimerTick;
            LevelTimer.Start();
        }

    }
}
