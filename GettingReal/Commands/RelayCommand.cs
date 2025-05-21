using System.Windows.Input;

namespace GettingReal.Commands 
{
    class RelayCommand : ICommand
    {
        // Fields
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        private Action<object> executeShowPackagingViewCommand;

        public RelayCommand(Action<object> executeShowPackagingViewCommand)
        {
            this.executeShowPackagingViewCommand = executeShowPackagingViewCommand;
        }

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }




        // ICommand implementation
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }


    }
    }
