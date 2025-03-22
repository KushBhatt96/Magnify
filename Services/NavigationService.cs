using Magnify.Interfaces.Services;
using Magnify.Model.Stores;
using Magnify.ViewModel;
using System;

namespace Magnify.Services
{
    public class NavigationService : INavigationService
    {
        private static INavigationService? _instance;
        private static readonly object _lock = new object(); 

        private readonly NavigationStack _firstNavigationStack;
        private readonly NavigationStack _secondNavigationStack;

        public event EventHandler? NavigationChanged;

        private NavigationService(BaseViewModel? initialViewModel)
        {
            _firstNavigationStack = new NavigationStack();
            _secondNavigationStack = new NavigationStack();

            if(initialViewModel != null)
            {
                _firstNavigationStack.Push(new NavigationNode(initialViewModel));
            }
        }

        public static INavigationService GetInstance(BaseViewModel? viewModel = null)
        {
            if(_instance == null)
            {
                lock (_lock)
                {
                    _instance = new NavigationService(viewModel);
                }
            }
            return _instance;
        }

        public static void SetInstance(BaseViewModel viewModel)
        {
            if (viewModel != null)
            {
                _instance = new NavigationService(viewModel);
            }
        }

        public void Navigate(BaseViewModel viewModel)
        {
            if (viewModel is null || viewModel == _firstNavigationStack.Peek())
            {
                return;
            }

            _firstNavigationStack.Push(new NavigationNode(viewModel));
            _secondNavigationStack.Clear();
            RaiseNavigationChangedEvent();
        }

        public void NavigateBack()
        {
            if (_firstNavigationStack.Count <= 1) // Do not pop if only single navigation node remaining
            {
                return;
            }
            var node = _firstNavigationStack.Pop();
            if (node != null)
            {
                _secondNavigationStack.Push(node);
                RaiseNavigationChangedEvent();
            }
        }

        public bool CanNavigateBack() => _firstNavigationStack.Count > 1;

        public void NavigateForward()
        {
            var node = _secondNavigationStack.Pop();
            if (node != null)
            {
                _firstNavigationStack.Push(node);
                RaiseNavigationChangedEvent();
            }
        }

        public bool CanNavigateForward() => _secondNavigationStack.Count > 0;

        public BaseViewModel? CurrentNavigationState()
        {
            return _firstNavigationStack.Peek();
        }

        private void RaiseNavigationChangedEvent()
        {
            NavigationChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
