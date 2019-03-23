using System;
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
        private Action<PlayableObject>AddItemOnScreen { get; set; }
        private int MapWidth { get; set; }
        private List<Player> Players { get; set; }
        private List<PlayableObject> ItemsInGame { get; set; } = new List<PlayableObject>();
        public event Action LevelFinishedEvent;
        public Level(List<Player> players, Difficulty difficulty, MapInfo mapInfo, Action<PlayableObject> ItemsOnScreen)
        {

            this.AddItemOnScreen = ItemsOnScreen;
            MapWidth = mapInfo.Width;
            Players = players;
            foreach(Player player in Players)
            {
                player.GoToStartPosition();
            }


            List<int> Xposittions = new List<int>();
            int rowPosition ;
            int movement;
            int width;
            int height;

            AddItem(new Water(0, mapInfo.Scale, mapInfo.Width - 1, (mapInfo.Scale * 3) - 1, players));
            int PodSize = mapInfo.Scale - 1;
            //AddItem(new Pod(mapInfo.Scale, 0, PodSize, PodSize, players));
            //AddItem(new Pod(mapInfo.Scale * 4, 0, PodSize, PodSize, players));
            AddItem(new Pod(mapInfo.Scale * 7, 0, PodSize, PodSize, players));
            //AddItem(new Pod(mapInfo.Scale * 10, 0, PodSize, PodSize, players));
            //AddItem(new Pod(mapInfo.Scale * 13, 0, PodSize, PodSize, players));

            Pod.AllPodsOccupied += LevelFinished;

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
        void AddItem(PlayableObject item)
        {
            ItemsInGame.Add(item);
            AddItemOnScreen(item);
        }
        void LevelFinished()
        {
            Pod.AllPodsOccupied -= LevelFinished;
            foreach (PlayableObject item in ItemsInGame)
            {
                item.Die();
            }
            ItemsInGame.Clear();
            LevelFinishedEvent?.Invoke();
            Players.Clear();
        }

    }
}
