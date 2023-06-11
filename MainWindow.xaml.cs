using Magnify.Data;
using Magnify.Interfaces;
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
        private MainViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            var messenger = Messenger.Instance;
            _viewModel = new MainViewModel(new DashboardViewModel(messenger),
                new ProjectsViewModel(new ProjectDataProvider(), messenger), new WorkItemsViewModel());

            DataContext = _viewModel;

            Loaded += MainWindow_Loaded;
        }

        public async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

        public void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            AdjustWindowSize();
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AdjustWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }

        }
    }
}
