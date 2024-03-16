using Magnify.ViewModel;
using System;

namespace Magnify.Interfaces.Services
{
    public interface INavigationService
    {
        event EventHandler? NavigationChanged;

        void Navigate(BaseViewModel viewModel);

        void NavigateBack();

        bool CanNavigateBack();

        void NavigateForward();

        bool CanNavigateForward();

        void SetInitialNavigationState(BaseViewModel viewModel);

        BaseViewModel? CurrentNavigationState();
    }
}
