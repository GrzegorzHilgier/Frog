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
            Players.Add(new Player(3, 100, 100, 1));

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
            Players[0].Xcoord -= 1*Scale;

        }
        void MoveRight()
        {
            Players[0].Xcoord += 1 * Scale;

        }
        void MoveUp()
        {
            Players[0].Ycoord -= 1 * Scale;

        }
        void MoveDown()
        {
            Players[0].Ycoord += 1 * Scale;
        }
    }
}
