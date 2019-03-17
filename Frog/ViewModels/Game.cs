using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Frog.Models;
using Frog.Utilities;

namespace Frog.ViewModels
{
    class Game : ObservableObject
    {
        public static int Scale = 30;

        public ObservableCollection<DrawableObject> Players { get; private set; } = new ObservableCollection<DrawableObject>();
        public ObservableCollection<DrawableObject> Enemies { get; private set; } = new ObservableCollection<DrawableObject>();

        public Game()
        {
            Players.Add(new Player(3, 0, 0, Scale, Scale));
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
                Players[0].Xcoord -= 1 * Scale;
            }
        }

        void MoveRight()
        {
            if(Players[0].Xcoord <= 360 - Players[0].Width)
            {
                Players[0].Xcoord += 1 * Scale;
            }
 

        }
        void MoveUp()
        {
            if(Players[0].Ycoord >= Players[0].Height)
            {
                Players[0].Ycoord -= 1 * Scale;
            }

        }
        void MoveDown()
        {

            if (Players[0].Ycoord <= 240 - Players[0].Height)
            {
                Players[0].Ycoord += 1 * Scale;
            }
        }
    }
}
