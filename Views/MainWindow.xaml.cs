using ElectionData.ViewModels;
using System.Windows;

namespace ElectionData.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void ReloadData_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            viewModel?.LoadData();  // Call LoadData again to reload the data
        }
    }
}
