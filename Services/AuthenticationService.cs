using Magnify.Data;
using Magnify.Interfaces.Services;
using Magnify.ViewModel;
using System;
using System.Windows;

namespace Magnify.Services
{
    /// <summary>
    /// Class <c>AuthenticationService</c> is a singleton service that allows a user to login/logout and then redirects the user.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private static AuthenticationService? _instance;

        public event Action<BaseViewModel>? RedirectionOccurred;

        private AuthenticationService() { }

        public static AuthenticationService Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new AuthenticationService();
                }
                return _instance;
            }
        }

        #region Public Instance Methods

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

        #endregion

        #region Private Helpers
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
        #endregion
    }
}
