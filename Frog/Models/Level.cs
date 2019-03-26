﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frog.Models;
using Frog.Utilities;
using System.Windows.Threading;

namespace Frog.Models
{
    class Level
    {
        private const int LevelMaxTime = 60;
        private int MapWidth { get; set; }

        private DispatcherTimer LevelTimer { get; set; } = new DispatcherTimer();

        private List<Player> Players { get; set; }
        private List<DrawableObject> ItemsInGame { get; set; } = new List<DrawableObject>();

        private Action<DrawableObject> AddItemOnScreen { get; set; }

        public int LevelTime { get; private set; } = LevelMaxTime;
        public event Action LevelTimeChangedEvent;
        public event Action<bool> LevelFinishedEvent;

        public Level(List<Player> players, Difficulty difficulty, MapInfo mapInfo, Action<DrawableObject> ItemsOnScreen)
        {


            this.AddItemOnScreen = ItemsOnScreen;
            MapWidth = mapInfo.Width;
            Players = players;
            foreach(Player player in Players)
            {
                player.GoToStartPosition();
                player.OutOfLives += PlayerLost;
            }

            List<int> Xposittions = new List<int>();
            int rowPosition;
            int movement;
            int width;
            int height;

            AddItem(new Water(0, mapInfo.Scale, mapInfo.Width - 1, (mapInfo.Scale * 3) - 1, players));
            AddItem(new Backgroud(0, 0, mapInfo.Width - 1, mapInfo.Scale  - 1, Backgroud.Type.GRASS));
            AddItem(new Backgroud(0, mapInfo.Scale * 5, mapInfo.Width - 1, mapInfo.Scale, Backgroud.Type.ROAD));
            AddItem(new Backgroud(0, mapInfo.Scale * 6, mapInfo.Width - 1, mapInfo.Scale, Backgroud.Type.ROAD));
            AddItem(new Backgroud(0, mapInfo.Scale * 7, mapInfo.Width - 1, mapInfo.Scale, Backgroud.Type.ROAD));
            int PodSize = mapInfo.Scale - 1;
            //AddItem(new Pod(mapInfo.Scale, 0, PodSize, PodSize, players));
            AddItem(new Pod(mapInfo.Scale * 4, 0, PodSize, PodSize, players));
            AddItem(new Pod(mapInfo.Scale * 7, 0, PodSize, PodSize, players));
           //AddItem(new Pod(mapInfo.Scale * 10, 0, PodSize, PodSize, players));
            //AddItem(new Pod(mapInfo.Scale * 13, 0, PodSize, PodSize, players));

            Pod.AllPodsOccupied += LevelFinished;
            Pod.PlayerScored += GivePoints; 

            rowPosition = mapInfo.Scale;
            movement = (int)difficulty;
            width = mapInfo.Scale * 2 - 1;
            height = mapInfo.Scale - 1;
            Xposittions.Add(mapInfo.Scale * 2);
            Xposittions.Add(mapInfo.Scale * 6);
            Xposittions.Add(mapInfo.Scale * 12);
            GenerateWoodRow(rowPosition, movement, width, height, Xposittions);

            rowPosition = mapInfo.Scale*2;
            movement = -((int)difficulty + 1);
            width = mapInfo.Scale * 4 - 1;
            height = mapInfo.Scale - 1;
            Xposittions.Clear();
            Xposittions.Add(mapInfo.Scale * 2);
            Xposittions.Add(mapInfo.Scale * 10);
            GenerateWoodSunkRow(rowPosition, movement, width, height, Xposittions);

            rowPosition = mapInfo.Scale * 3;
            movement = (int)difficulty;
            width = mapInfo.Scale * 2 - 1;
            height = mapInfo.Scale - 1;
            Xposittions.Clear();
            Xposittions.Add(mapInfo.Scale * 1);
            Xposittions.Add(mapInfo.Scale * 5);
            Xposittions.Add(mapInfo.Scale * 13);
            GenerateWoodRow(rowPosition, movement, width, height, Xposittions);

            rowPosition = mapInfo.Scale * 5;
            movement = -((int)difficulty);
            width = mapInfo.Scale - 1;
            height = mapInfo.Scale - 1;
            Xposittions.Clear();
            Xposittions.Add(mapInfo.Scale);
            Xposittions.Add(mapInfo.Scale * 5);
            Xposittions.Add(mapInfo.Scale * 9);
            Xposittions.Add(mapInfo.Scale * 13);
            GenerateCarRow(rowPosition, movement, width, height, Xposittions);

            rowPosition = mapInfo.Scale * 6;
            movement = (int)difficulty + 1;
            width = mapInfo.Scale - 1;
            height = mapInfo.Scale - 1;
            Xposittions.Clear();
            Xposittions.Add(mapInfo.Scale);
            Xposittions.Add(mapInfo.Scale * 5);
            Xposittions.Add(mapInfo.Scale * 9);
            Xposittions.Add(mapInfo.Scale * 13);
            GenerateCarRow(rowPosition, movement, width, height, Xposittions);

            rowPosition = mapInfo.Scale * 7;
            movement = -((int)difficulty);
            width = mapInfo.Scale * (int)difficulty - 1;
            height = mapInfo.Scale - 1;
            Xposittions.Clear();
            Xposittions.Add(mapInfo.Scale * 2);
            Xposittions.Add(mapInfo.Scale * 7);
            Xposittions.Add(mapInfo.Scale * 12);
            GenerateCarRow(rowPosition, movement, width, height, Xposittions);

            LevelTimer.Interval = TimeSpan.FromSeconds(1);
            LevelTimer.Tick += TimerTick;
            LevelTimer.Start();

        }
        void GenerateWoodRow(int RowPosition, int movement, int width, int height, List<int> Xpositions)
        {
            foreach (int x in Xpositions)
            {
                AddItem(new Wood(x, RowPosition, width, height, movement, 0, MapWidth, Players));
            }

        }
        void GenerateCarRow(int RowPosition, int movement, int width, int height, List<int> Xpositions)
        {
            foreach (int x in Xpositions)
            {
                AddItem(new Car(x, RowPosition, width, height, movement, 0, MapWidth, Players));
            }
        }
        void GenerateWoodSunkRow(int RowPosition, int movement, int width, int height, List<int> Xpositions)
        {
            foreach (int x in Xpositions)
            {
                AddItem(new WoodSunk(x, RowPosition, width, height, movement, 0, MapWidth, Players));
            }
        }
        void AddItem(DrawableObject item)
        {
            ItemsInGame.Add(item);
            AddItemOnScreen(item);
        }
        void LevelFinished()
        {
            Pod.AllPodsOccupied -= LevelFinished;
            Pod.PlayerScored -= GivePoints;
            foreach (DrawableObject item in ItemsInGame)
            {
                item.Die();
            }
            LevelTimer.Tick -= TimerTick;
            LevelTimer.Stop();
            LevelTimer = null;
            ItemsInGame.Clear();
            RaiseLevelFinishedEvent(true);

        }
        void RaiseLevelFinishedEvent(bool PlayerWon)
        {
            LevelFinishedEvent?.Invoke(PlayerWon);
        }

        void TimerTick(object sender, EventArgs e)
        {

            LevelTime--;
            if (LevelTime == 0)
            {
                RaiseLevelFinishedEvent(false);
            }
            LevelTimeChangedEvent?.Invoke();           
        }
        void GivePoints(Player player)
        {
            player.Score += LevelTime * 10;
        }
        void PlayerLost(int Score)
        {
            if(Player.PlayersInGame==0)
            {
                RaiseLevelFinishedEvent(false);
            }

        }

    }
}
