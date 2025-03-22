using Magnify.Data;
using Magnify.Interfaces.Services;
using Magnify.Services;

namespace Magnify.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authService;

        private BaseViewModel? _selectedViewModel;
        public BaseViewModel? SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            // TODO: Change to LoginViewModel
            SelectedViewModel = new HomeViewModel(
                new DashboardViewModel(), new ProjectsViewModel(ProjectDataProvider.Instance),
                new WorkItemsViewModel(), new StoryBoardViewModel(),
                new ChatViewModel()
                );
            // TODO: Remove this line after changing to LoginViewModel
            SelectedViewModel?.LoadAsync();

            _authService = AuthenticationService.Instance;
            _authService.RedirectionOccurred += User_RedirectAfterLogin;
        }

        public void User_RedirectAfterLogin(BaseViewModel viewModel)
        {
            SelectedViewModel = viewModel;
            SelectedViewModel?.LoadAsync();
        }
    }
}
