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
    class ItemsFactory
    {
        public ItemsFactory(List<Player> players, Action<DrawableObject> ItemsOnScreen,MapInfo mapInfo)
        {
            ItemsOnScreen(new Water(0, mapInfo.Scale, mapInfo.Width - 1, (mapInfo.Scale * 3) - 1, players));
            ItemsOnScreen(new Pod(mapInfo.Scale, 0, mapInfo.Scale - 1, mapInfo.Scale - 1, players));
            ItemsOnScreen(new Pod(mapInfo.Scale * 4, 0, mapInfo.Scale - 1, mapInfo.Scale - 1, players));
            ItemsOnScreen(new Pod(mapInfo.Scale * 7, 0, mapInfo.Scale - 1, mapInfo.Scale - 1, players));
            ItemsOnScreen(new Pod(mapInfo.Scale * 10, 0, mapInfo.Scale - 1, mapInfo.Scale - 1, players));
            ItemsOnScreen(new Pod(mapInfo.Scale * 13, 0, mapInfo.Scale - 1, mapInfo.Scale - 1, players));

            ItemsOnScreen(new Wood(mapInfo.Scale * 2, mapInfo.Scale, mapInfo.Scale * 2 - 1, mapInfo.Scale - 1, -2, 0, mapInfo.Width, players));
            ItemsOnScreen(new Wood(mapInfo.Scale * 7, mapInfo.Scale, mapInfo.Scale * 2 - 1, mapInfo.Scale - 1, -2, 0, mapInfo.Width, players));
            ItemsOnScreen(new Wood(mapInfo.Scale * 12, mapInfo.Scale, mapInfo.Scale * 2 - 1, mapInfo.Scale - 1, -2, 0, mapInfo.Width, players));

            ItemsOnScreen(new Wood(mapInfo.Scale * 2, mapInfo.Scale * 2, mapInfo.Scale * 4 - 1, mapInfo.Scale - 1, 3, 0, mapInfo.Width, players));
            ItemsOnScreen(new Wood(mapInfo.Scale * 10, mapInfo.Scale * 2, mapInfo.Scale * 4 - 1, mapInfo.Scale - 1, 3, 0, mapInfo.Width, players));

            ItemsOnScreen(new Wood(mapInfo.Scale * 2, mapInfo.Scale * 3, mapInfo.Scale * 2 - 1, mapInfo.Scale - 1, -2, 0, mapInfo.Width, players));
            ItemsOnScreen(new Wood(mapInfo.Scale * 7, mapInfo.Scale * 3, mapInfo.Scale * 2 - 1, mapInfo.Scale - 1, -2, 0, mapInfo.Width, players));
            ItemsOnScreen(new Wood(mapInfo.Scale * 12, mapInfo.Scale * 3, mapInfo.Scale * 2 - 1, mapInfo.Scale - 1, -2, 0, mapInfo.Width, players));

            ItemsOnScreen(new Car(mapInfo.Scale, mapInfo.Scale *5, mapInfo.Scale - 1, mapInfo.Scale - 1, -2,0 ,mapInfo.Width, players));
            ItemsOnScreen(new Car(mapInfo.Scale *5, mapInfo.Scale * 5, mapInfo.Scale - 1, mapInfo.Scale - 1, -2, 0, mapInfo.Width, players));
            ItemsOnScreen(new Car(mapInfo.Scale *9, mapInfo.Scale * 5, mapInfo.Scale - 1, mapInfo.Scale - 1, -2, 0, mapInfo.Width, players));
            ItemsOnScreen(new Car(mapInfo.Scale * 13, mapInfo.Scale * 5, mapInfo.Scale - 1, mapInfo.Scale - 1, -2, 0, mapInfo.Width, players));

            ItemsOnScreen(new Car(mapInfo.Scale, mapInfo.Scale * 6, mapInfo.Scale - 1, mapInfo.Scale - 1, 3, 0, mapInfo.Width, players));
            ItemsOnScreen(new Car(mapInfo.Scale * 5, mapInfo.Scale * 6, mapInfo.Scale - 1, mapInfo.Scale - 1, 3, 0, mapInfo.Width, players));
            ItemsOnScreen(new Car(mapInfo.Scale * 9, mapInfo.Scale * 6, mapInfo.Scale - 1, mapInfo.Scale - 1, 3, 0, mapInfo.Width, players));
            ItemsOnScreen(new Car(mapInfo.Scale * 13, mapInfo.Scale * 6, mapInfo.Scale - 1, mapInfo.Scale - 1, 3, 0, mapInfo.Width, players));

            ItemsOnScreen(new Car(mapInfo.Scale * 2, mapInfo.Scale * 7, mapInfo.Scale*2 - 1, mapInfo.Scale - 1, -2, 0, mapInfo.Width, players));
            ItemsOnScreen(new Car(mapInfo.Scale * 7, mapInfo.Scale * 7, mapInfo.Scale*2 - 1, mapInfo.Scale - 1, -2, 0, mapInfo.Width, players));
            ItemsOnScreen(new Car(mapInfo.Scale * 12, mapInfo.Scale * 7, mapInfo.Scale*2 - 1, mapInfo.Scale - 1, -2, 0, mapInfo.Width, players));


        }

    }
}
