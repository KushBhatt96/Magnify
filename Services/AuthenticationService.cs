using Magnify.Data;
using Magnify.Interfaces.Services;
using Magnify.Model.Stores;
using Magnify.ViewModel;
using System;
using System.Windows;

namespace Magnify.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private static AuthenticationService? _instance = null;

        public event Action<BaseViewModel>? RedirectionOccurred;

        private AuthenticationService() { }

        public static AuthenticationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AuthenticationService();
                }
                return _instance;
            }
        }

        public void Login(string nu, string wp)
        {
            MessageBox.Show($"Logging in {nu}...");

            // TODO: Login flow here

            RedirectAfterLogin();
        }

        public void Logout()
        {
            MessageBox.Show($"Logging out...");

            // TODO: Logout flow here

            RedirectAfterLogout();
        }

        private void RedirectAfterLogin()
        {
            RaiseRedirectionOccurred(new HomeViewModel(
                new DashboardViewModel(), new ProjectsViewModel(ProjectDataProvider.Instance),
                new WorkItemsViewModel(), new StoryBoardViewModel(), 
                new ChatViewModel()
                ));
        }

        private void RedirectAfterLogout()
        {
            RaiseRedirectionOccurred(new LoginViewModel());
        }

        private void RaiseRedirectionOccurred(BaseViewModel viewModel)
        {
            RedirectionOccurred?.Invoke(viewModel);
        }
    }
}
