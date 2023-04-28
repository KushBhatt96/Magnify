using Magnify.Data;
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

            _viewModel = new MainViewModel(new DashboardViewModel(),
                new ProjectsViewModel(new ProjectDataProvider()), new WorkItemsViewModel());

            DataContext = _viewModel;

            Loaded += MainWindow_Loaded;
        }

        public async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

    }
}
