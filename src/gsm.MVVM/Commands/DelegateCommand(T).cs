using System;
using System.Windows.Input;

namespace gsm.MVVM.Commands
{
    public class DelegateCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private readonly Action<T> _executeAction;
        private readonly Func<T, bool> _canExecuteFunc;

        public DelegateCommand(Action<T> executeAction)
            : this(executeAction, (o) => true)
        {

        }

        public DelegateCommand(Action<T> executeAction, Func<T, bool> canExecuteFunc)
        {
            _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
            _canExecuteFunc = canExecuteFunc ?? throw new ArgumentNullException(nameof(canExecuteFunc));
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteFunc((T)parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction((T)parameter);
        }
    }
}