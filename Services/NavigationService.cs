using Magnify.Interfaces.Services;
using Magnify.Model.Stores;
using Magnify.ViewModel;
using System;

namespace Magnify.Services
{
    public class NavigationService : INavigationService
    {
        private static NavigationService? _instance = null;

        private readonly NavigationStore _navigationStore;

        public event EventHandler? NavigationChanged;

        private NavigationService(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public static NavigationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NavigationService(new NavigationStore());
                }
                return _instance;
            }

        }

        public void Navigate(BaseViewModel viewModel)
        {
            if (viewModel is null)
            {
                return;
            }

            _navigationStore.SelectedViewModel = viewModel;
            RaiseNavigationChangedEvent();
        }

        public BaseViewModel? CurrentNavigationState()
        {
            return _navigationStore.SelectedViewModel;
        }

        private void RaiseNavigationChangedEvent()
        {
            NavigationChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
