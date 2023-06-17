using Magnify.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnify.Model.Stores
{
    public class NavigationStore
    {

        // TODO: Implement own "Stack" structure where we can remove from the bottom
        //       to ensure that navigation history does not exceed 10
        private const int MaxNavigationHistory = 10;

        private readonly Stack<BaseViewModel> _firstNavigationStack;

        private readonly Stack<BaseViewModel> _secondNavigationStack;

        public BaseViewModel SelectedViewModel
        {
            get => _firstNavigationStack.Peek();
            set
            {
                if(_firstNavigationStack.Count != 0)
                {
                    if (value == _firstNavigationStack.Peek())
                    {
                        return;
                    }
                }

                _firstNavigationStack.Push(value);

                if (_secondNavigationStack.Count > 0)
                {
                    _secondNavigationStack.Clear();
                }

                RaiseNavigationChanged();
            }
        }

        public void PreviousViewModel()
        {
            if (_firstNavigationStack.Count > 1)
            {
                var currentState = _firstNavigationStack.Pop();
                _secondNavigationStack.Push(currentState);
                RaiseNavigationChanged();
            }
        }

        public void NextViewModel()
        {
            if (_secondNavigationStack.Count > 0)
            {
                var currentState = _secondNavigationStack.Pop();
                _firstNavigationStack.Push(currentState);
                RaiseNavigationChanged();
            }
        }

        public Action? NavigationChanged;


        public NavigationStore()
        {
            _firstNavigationStack = new Stack<BaseViewModel>();
            _secondNavigationStack = new Stack<BaseViewModel>();
        }

        public void RaiseNavigationChanged()
        {
            // if navigationChanged event is non-null, this means it has some event handlers subscribed to it
            NavigationChanged?.Invoke();
        }

        public int FirstStackCount() => _firstNavigationStack.Count;

        public int SecondStackCount() => _secondNavigationStack.Count;

    }
}
