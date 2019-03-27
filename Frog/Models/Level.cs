using System;
using System.Collections.Generic;
using Frog.Utilities;
using System.Windows.Threading;

namespace Frog.Models
{
    abstract class Level
    {
        protected virtual int LevelMaxTime { get; set; } = 20;
        private DispatcherTimer LevelTimer { get; set; } = new DispatcherTimer();
        private List<DrawableObject> ItemsInGame { get; set; } = new List<DrawableObject>();
        private Action<DrawableObject> AddItemToGame { get; set; }
        protected List<Player> Players { get; set; }
        private int levelTime { get; set; }
        public int LevelTime
        {
            get=>levelTime;
            protected set
            {
                levelTime = value;
                LevelTimeChangedEvent?.Invoke();
            }
        } 
        public event Action LevelTimeChangedEvent;
        public event Action<bool> LevelFinishedEvent;

        protected void AddItem(DrawableObject item)
        {
            ItemsInGame.Add(item);
            AddItemToGame(item);
        }

        protected virtual void LevelFinished()
        {
            RaiseLevelFinishedEvent(true);
        }

        void PlayerLostLife(Player player)
        {
            player.GoToStartPosition();
            ResetTime();
        }
        void PlayerGameOver(Player player)
        {
            if (Player.PlayersInGame == 0)
            {
                RaiseLevelFinishedEvent(false);
            }

        }

        void RaiseLevelFinishedEvent(bool PlayerWon)
        {

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

            LevelFinishedEvent?.Invoke(PlayerWon);
        }

        void TimerTick(object sender, EventArgs e)
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
