using Magnify.ViewModel;
using System.Windows;

namespace Magnify
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;
        public MainWindow()
        {
            InitializeComponent();

            _mainViewModel = new MainViewModel();

            DataContext = _mainViewModel;
        }
    }
}
