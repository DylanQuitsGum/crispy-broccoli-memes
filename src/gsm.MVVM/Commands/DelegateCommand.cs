using System;
using System.Windows.Input;

namespace gsm.MVVM.Commands
{
    public class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                _weakReferenceManager.Add(value);
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                _weakReferenceManager.Remove(value);
                CommandManager.RequerySuggested -= value;
            }
        }

        private readonly Action _executeAction;
        private readonly Func<bool> _canExecuteFunc;
        private readonly WeakReferenceManager _weakReferenceManager = new WeakReferenceManager();

        public DelegateCommand(Action executeAction)
            : this(executeAction, () => true)
        {

        }

        public DelegateCommand(Action executeAction, Func<bool> canExecuteFunc)
        {
            _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
            _canExecuteFunc = canExecuteFunc ?? throw new ArgumentNullException(nameof(canExecuteFunc));
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteFunc();
        }

        public void Execute(object parameter)
        {
            _executeAction();
        }

        protected virtual void OnCanExecuteChanged()
        {
            _weakReferenceManager.CallHandlers(this);
        }
    }
}