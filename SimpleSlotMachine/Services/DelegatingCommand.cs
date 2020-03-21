using System;
using System.Windows.Input;

namespace SimpleSlotMachine.Services
{
    class DelegatingCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Func<object, bool> _canExecute;

        public DelegatingCommand(Action action) : this((o) => action())
        {

        }

        public DelegatingCommand(Action<object> action) : this(action, (o) => true)
        {

        }

        public DelegatingCommand(Action<object> action, Func<object, bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

    }
}
