using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Frog.Models;
using Frog.Utilities;

namespace Frog.ViewModels
{
    class Game : ObservableObject
    {

        const int Scale = 30;
        const int MAP_HEIGHT = Scale * 9;
        const int MAP_WIDTH = Scale * 15;

        public ObservableCollection<Player> Players { get; private set; } = new ObservableCollection<Player>();
        public ObservableCollection<DrawableObject> ItemsOnScreen { get; private set; } = new ObservableCollection<DrawableObject>();

        public Game()
        {
            //TODO add more players
            Players.Add(new Player(3, Scale*6, Scale*8, Scale, Scale));

            List <Player> players = new List<Player>();
            foreach(Player player in Players)
            {
                players.Add(player);
            }

            ItemsOnScreen.Add(new Water(0, Scale, MAP_WIDTH, Scale*3));
            ItemsOnScreen.Add(new Pod(Scale, 0, Scale, Scale,players));
            ItemsOnScreen.Add(new Pod(Scale * 4, 0, Scale, Scale,players));
            ItemsOnScreen.Add(new Pod(Scale * 7, 0, Scale, Scale,players));
            ItemsOnScreen.Add(new Pod(Scale * 10, 0, Scale, Scale,players));
            ItemsOnScreen.Add(new Pod(Scale * 13, 0, Scale, Scale,players));
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
            if(Players[0].Xcoord >= Players[0].Width)
            {
                //Players[0].Xcoord -= 1 * Scale;
                //Players[0].RaiseObjectMovedEvent();

                Players[0].TryToMove(Direction.LEFT, Scale);
            }
        }

        void MoveRight()
        {
            if(Players[0].Xcoord < MAP_WIDTH - Players[0].Width)
            {
                //Players[0].Xcoord += 1 * Scale;
                //Players[0].RaiseObjectMovedEvent();

                Players[0].TryToMove(Direction.RIGHT, Scale);
            }

        }

        void MoveUp()
        {
            if(Players[0].Ycoord >= Players[0].Height)
            {
                //Players[0].Ycoord -= 1 * Scale;
                //Players[0].RaiseObjectMovedEvent();

                Players[0].TryToMove(Direction.UP, Scale);
            }

        }

        void MoveDown()
        {
            if (Players[0].Ycoord < MAP_HEIGHT- Players[0].Height)
            {
                //Players[0].Ycoord += 1 * Scale;
                //Players[0].RaiseObjectMovedEvent();

                Players[0].TryToMove(Direction.DOWN, Scale);
            }
        }
    }
}
