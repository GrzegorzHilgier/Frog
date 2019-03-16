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
        Player Player1 { get; set; } = new Player(3, 0, 0, 1);
        public int X { get => Player1.Xcoord; }
        public int Y { get => Player1.Ycoord; }
        public ushort Lives { get => Player1.Lives; }
        public int Score { get => Player1.Score; }

        public ObservableCollection<DrawableObject> ItemsToDraw { get; private set; } = new ObservableCollection<DrawableObject>();

        public Game()
        {

            ItemsToDraw.Add(new Player(3, 0, 0, 1));
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
            Player1.Xcoord -= 1*Scale;
            RaisePropertyChangedEvent("X");
        }
        void MoveRight()
        {
            Player1.Xcoord += 1 * Scale;
            RaisePropertyChangedEvent("X");
        }
        void MoveUp()
        {
            Player1.Ycoord -= 1 * Scale;
            RaisePropertyChangedEvent("Y");
        }
        void MoveDown()
        {
            Player1.Ycoord += 1 * Scale;
            RaisePropertyChangedEvent("Y");
        }
    }
}
