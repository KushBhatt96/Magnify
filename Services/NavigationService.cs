using Magnify.Interfaces.Services;
using Magnify.Model.Stores;
using Magnify.ViewModel;
using System;
using System.Windows;

namespace Magnify.Services
{
    public class NavigationService : INavigationService
    {
        private static NavigationService? _instance = null;

        private readonly NavigationStack _firstNavigationStack;
        private readonly NavigationStack _secondNavigationStack;

        public event EventHandler? NavigationChanged;

        private NavigationService()
        {
            _firstNavigationStack = new NavigationStack();
            _secondNavigationStack = new NavigationStack();
        }

        public static NavigationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NavigationService();
                }
                return _instance;
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

        public void SetInitialNavigationState(BaseViewModel viewModel)
        {
            if (viewModel == null)
            {
                return;
            }

            _firstNavigationStack.Push(new NavigationNode(viewModel));
        }

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
