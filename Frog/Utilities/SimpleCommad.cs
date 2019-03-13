using System;
using System.Windows.Input;

namespace Frog.ViewModels
{
    class SimpleCommand : ICommand
    {
        private readonly Action _action;

        public SimpleCommand(Action action)
        {
            _action = action;
        }
        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {

            }

            remove
            {

            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return true;
        }

        void ICommand.Execute(object parameter)
        {
            _action();
        }
    }
}
