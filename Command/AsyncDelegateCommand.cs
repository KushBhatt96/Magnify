using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Magnify.Command
{
    public class AsyncDelegateCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Func<object?, Task> _executeAsync;

        private bool _isExecuting;
        public bool IsExecuting
        {
            get => _isExecuting;
            set
            {
                _isExecuting = value;
                RaiseCanExecuteChanged();
            }
        }

        public AsyncDelegateCommand(Func<object?, Task> executeAsync)
        {
            _executeAsync = executeAsync;
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object? parameter) => !IsExecuting;

        public async void Execute(object? parameter)
        {
            
            try
            {
                IsExecuting = true;
                await _executeAsync(parameter);
            }
            catch
            {
                // TODO: Improve exception handling here
                MessageBox.Show("Error occurred during async operation.");
            }
            finally
            {
                IsExecuting = false;
            }
        }
    }
}
