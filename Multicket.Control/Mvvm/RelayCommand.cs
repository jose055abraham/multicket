using Core.Event;
using System;
using System.Windows.Input;

namespace Multicket.Module.Mvvm
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> action;
        private readonly Predicate<T> execute;

        public RelayCommand(Action<T> action) : this(action, null)
        {
            this.action = action;
        }

        public RelayCommand(Action<T> action, Predicate<T> execute)
        {
            this.action = action ?? throw new ArgumentException("execute");
            this.execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return execute == null || execute((T)parameter);
        }

        public void Execute(object parameter)
        {
            action((T)parameter);
        }

        private event EventHandler CanExecuteChangedInternal;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChangedInternal.Raise(this);
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> execute;
        private readonly Action<object> action;

        public RelayCommand(Action<object> action) : this(action, null) { }

        public RelayCommand(Action<object> action, Predicate<object> execute)
        {
            this.action = action ?? throw new ArgumentException("execute"); ;
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return execute == null || execute(parameter);
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; CanExecuteChangedInternal += value; }
            remove { CommandManager.RequerySuggested -= value; CanExecuteChangedInternal -= value; }
        }

        private event EventHandler CanExecuteChangedInternal;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChangedInternal.Raise(this);
        }
    }

    public abstract class RelayCommandBase : ICommand
    {
        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public class InvokeCommand : RelayCommandBase
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public InvokeCommand() { }

        public InvokeCommand(Action execute) : this(execute, null) { }

        public InvokeCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public override void Execute(object parameter)
        {
            _execute();
        }
    }

    public class InvokeCommand<T> : InvokeCommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public InvokeCommand(Action<T> execute) : this(execute, null) { }

        public InvokeCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public override void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
