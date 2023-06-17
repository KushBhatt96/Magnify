using Magnify.ViewModel;
using System;

namespace Magnify.Interfaces.Services
{
    public interface INavigationService
    {
        event EventHandler? NavigationChanged;

        void Navigate(BaseViewModel viewModel);

        BaseViewModel? CurrentNavigationState();
    }
}
