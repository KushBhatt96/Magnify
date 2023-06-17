using Magnify.Data;
using Magnify.Interfaces;
using Magnify.Model.Stores;
using Magnify.Services;
using Magnify.ViewModel;
using System.Windows;

namespace Magnify
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _mainViewModel;
        public MainWindow()
        {
            InitializeComponent();

            var messenger = MessengerService.Instance;
            var authRedirectionStore = new NavigationStore();
            var navigationStore = new NavigationStore();

            var dashboardViewModel = new DashboardViewModel(messenger);
            var projectsViewModel = new ProjectsViewModel(new ProjectDataProvider(), messenger, navigationStore);
            var workItemsViewModel = new WorkItemsViewModel();



            var homeViewModel = new HomeViewModel(dashboardViewModel, projectsViewModel, workItemsViewModel, navigationStore);
            var loginViewModel = new LoginViewModel(homeViewModel, authRedirectionStore);


            _mainViewModel = new MainViewModel(loginViewModel, homeViewModel, authRedirectionStore);

            DataContext = _mainViewModel;
        }

        //public void MinimizeButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.WindowState = WindowState.Minimized;
        //}

        //public void MaximizeButton_Click(object sender, RoutedEventArgs e)
        //{
        //    AdjustWindowSize();
        //}

        //public void CloseButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Application.Current.Shutdown();
        //}

        //private void AdjustWindowSize()
        //{
        //    if (this.WindowState == WindowState.Maximized)
        //    {
        //        this.WindowState = WindowState.Normal;
        //    }
        //    else
        //    {
        //        this.WindowState = WindowState.Maximized;
        //    }
        //}
    }
}
