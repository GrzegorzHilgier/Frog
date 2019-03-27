using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frog.Utilities;
using Frog.Models;

namespace Frog.Levels
{
    class Level1:Level
    {
        public override void Init(List<Player> players, MapInfo mapInfo, Action<DrawableObject> addItemToGame)
        {
            base.Init(players, mapInfo, addItemToGame);

            AddItem(new Water(0, mapInfo.Scale, mapInfo.Width - 1, (mapInfo.Scale * 3) - 1, players));
            AddItem(new Backgroud(0, 0, mapInfo.Width - 1, mapInfo.Scale - 1, Backgroud.Type.GRASS));
            AddItem(new Backgroud(0, mapInfo.Scale * 5, mapInfo.Width - 1, mapInfo.Scale, Backgroud.Type.ROAD));
            AddItem(new Backgroud(0, mapInfo.Scale * 6, mapInfo.Width - 1, mapInfo.Scale, Backgroud.Type.ROAD));
            AddItem(new Backgroud(0, mapInfo.Scale * 7, mapInfo.Width - 1, mapInfo.Scale, Backgroud.Type.ROAD));
            int PodSize = mapInfo.Scale - 1;

           //AddItem(new Pod(mapInfo.Scale * 4, 0, PodSize, PodSize, players));
            AddItem(new Pod(mapInfo.Scale * 7, 0, PodSize, PodSize, players));
           //AddItem(new Pod(mapInfo.Scale * 10, 0, PodSize, PodSize, players));

            Pod.AllPodsOccupied += LevelFinished;
            Pod.PlayerScored += GivePoints;
            
            int Scale = mapInfo.Scale;
            int height = mapInfo.Scale - 1;

            AddItem(new WoodSunk(Scale, mapInfo.Scale, Scale * 3, height, 1, 0, mapInfo, Players));
            AddItem(new Wood(Scale * 6, mapInfo.Scale, Scale , height, 1, 0, mapInfo, Players));
            AddItem(new Wood(Scale * 12, mapInfo.Scale, Scale * 3, height, 1, 0, mapInfo, Players));

            AddItem(new Wood(Scale, Scale*2, Scale * 3, height, -1, 0, mapInfo, Players));
            AddItem(new Wood(Scale * 6, Scale*2, Scale, height, -1, 0, mapInfo, Players));
            AddItem(new Wood(Scale * 12, Scale*2, Scale*3, height, -1, 0, mapInfo, Players));

            AddItem(new Wood(Scale, Scale * 3, Scale * 2, height, 1, 0, mapInfo, Players));
            AddItem(new Wood(Scale * 5, Scale * 3, Scale*2, height, 1, 0, mapInfo, Players));
            AddItem(new Wood(Scale * 12, Scale * 3, Scale * 2, height, 1, 0, mapInfo, Players));

            AddItem(new Car(Scale, Scale * 5, Scale , height, 1, 0, mapInfo, Players));
            AddItem(new Car(Scale * 5, Scale * 5, Scale , height, 1, 0, mapInfo, Players));
            AddItem(new Car(Scale * 12, Scale * 5, Scale , height, 1, 0, mapInfo, Players));

            AddItem(new Car(Scale, Scale * 6, Scale, height, -2, 0, mapInfo, Players));
            AddItem(new Car(Scale * 5, Scale * 6, Scale, height, -2, 0, mapInfo, Players));
            AddItem(new Car(Scale * 12, Scale * 6, Scale, height, -2, 0, mapInfo, Players));

            AddItem(new Car(Scale, Scale * 7, Scale, height, -1, 0, mapInfo, Players));
            AddItem(new Car(Scale * 5, Scale * 7, Scale, height, -1, 0, mapInfo, Players));
            AddItem(new Car(Scale * 12, Scale * 7, Scale, height, -1, 0, mapInfo, Players));


        }
        protected override void LevelFinished()
        {
            Pod.AllPodsOccupied -= LevelFinished;
            Pod.PlayerScored -= GivePoints;
            base.LevelFinished();
        }
    }
}
