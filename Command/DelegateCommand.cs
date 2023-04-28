using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Magnify.Command
{
    public class DelegateCommand : ICommand
    {
        // _execute is a pointer to a method that accepts an object type, which we can run when a UI interaction occurs
        private readonly Action<object?> _execute;

        // _canExecute is a pointer to a method that accepts an object type and returns a bool, the bool determines the UI element's isEnabled property
        private readonly Func<object?, bool>? _canExecute;

        // CanExecuteChanged is an event that can be raised from a DeletegateCommand instance in our ViewModel
        // The ViewModel should call RaiseCanExecuteChanged when we wish to re-evaluate the isEnabled property of a certain UI element
        // When CanExecuteChanged event is raised, it will call the CanExecute method which will return a bool
        public event EventHandler? CanExecuteChanged;

        public DelegateCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object? parameter) => _canExecute is null || _canExecute(parameter);

        public void Execute(object? parameter) => _execute(parameter);
    }
}
