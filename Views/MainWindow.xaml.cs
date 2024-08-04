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

            // Subscribe to the PropertyChanged event to update footer visibility
        }

        private void ReloadData_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            viewModel?.LoadData();  // Call LoadData again to reload the data
        }

        private void MainWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is MainViewModel oldViewModel)
            {
                oldViewModel.PropertyChanged -= MainWindow_PropertyChanged;
            }

            if (e.NewValue is MainViewModel newViewModel)
            {
                newViewModel.PropertyChanged += MainWindow_PropertyChanged;
                UpdateFooterVisibility(newViewModel.TotalVotes);
            }
        }

        private void MainWindow_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TotalVotes")
            {
                var viewModel = DataContext as MainViewModel;
                UpdateFooterVisibility(viewModel.TotalVotes);
            }
        }

        private void UpdateFooterVisibility(decimal totalVotes)
        {
            bool isVisible = totalVotes == 10058774;
            FooterTextBlock.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
            FooterNoteTextBlock.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
