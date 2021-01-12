using System;
using System.Windows;
using System.Windows.Input;

namespace gsm.MVVM.Behaviors
{
    public class CommandBehaviorBase<T> where T : UIElement
    {
        private readonly EventHandler _commandCanExecuteChangedHandler;

        public CommandBehaviorBase(T targetObject)
        {
            _targetObject = new WeakReference(targetObject ?? throw new ArgumentNullException(nameof(targetObject)));

            _commandCanExecuteChangedHandler = CommandCanExecuteChanged;
        }

        private bool _autoEnabled = true;
        public bool AutoEnable
        {
            get => _autoEnabled;
            set
            {
                _autoEnabled = value;
                UpdateEnabledState();
            }
        }

        private ICommand _command;
        public ICommand Command
        {
            get => _command;
            set
            {
                if (_command != null)
                {
                    _command.CanExecuteChanged -= _commandCanExecuteChangedHandler;
                }

                _command = value;

                if (_command == null) return;

                _command.CanExecuteChanged += _commandCanExecuteChangedHandler;
                UpdateEnabledState();
            }
        }

        private object _commandParameter;
        public object CommandParameter
        {
            get => _commandParameter;
            set
            {
                if (_commandParameter == value) return;

                _commandParameter = value;
                UpdateEnabledState();
            }
        }

        private readonly WeakReference _targetObject;
        protected T TargetObject => _targetObject?.Target as T;

        protected virtual void UpdateEnabledState()
        {
            if (TargetObject == null)
            {
                Command = null;
                CommandParameter = null;
            }
            else if (Command != null)
            {
                if (AutoEnable)
                    TargetObject.IsEnabled = Command.CanExecute(CommandParameter);
            }
        }

        private void CommandCanExecuteChanged(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }

        protected virtual void ExecuteCommand(object parameter)
        {
                Command?.Execute(CommandParameter ?? parameter);
        }
    }
}
