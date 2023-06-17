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

            _mainViewModel = new MainViewModel();

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
